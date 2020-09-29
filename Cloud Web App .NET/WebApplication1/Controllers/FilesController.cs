using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Models;
using Google.Cloud.Storage.V1;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Diagnostics.AspNet;

namespace WebApplication1.Controllers
{
    public class FilesController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        { return View(); }

        [HttpPost]
        public ActionResult Create(File p, HttpPostedFileBase file)
        {
            //upload image related to file on the bucket
            string link = "";

            try
            {
                if(file!= null)
                { 
                #region Uploading file on Cloud Storage
                var storage = StorageClient.Create();

                    using (var f = file.InputStream)
                    {
                        var filename = Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                        var storageObject = storage.UploadObject("pfc1001", filename, null, f);
                        
                        link = "https://storage.cloud.google.com/pfc1001/"+ storageObject.Name;

                        if (null == storageObject.Acl)
                        {
                            storageObject.Acl = new List<ObjectAccessControl>();
                        }


                        storageObject.Acl.Add(new ObjectAccessControl()
                        {
                            Bucket = "pfc1001",
                            Entity = $"user-" + User.Identity.Name, //whereas sawser551@gmail.com has to be replaced by a gmail email address who you want to have access granted
                            Role = "OWNER", //READER
                        });

                        if (IsValidEmail(p.Recipient))
                        {
                            storageObject.Acl.Add(new ObjectAccessControl()
                            {
                                Bucket = "pfc1001",
                                Entity = $"user-" + p.Recipient, //whereas yalcin.formosa@gmail.com has to be replaced by a gmail email address who you want to have access granted
                                Role = "READER", //READER
                            });
                        }

                        var updatedObject = storage.UpdateObject(storageObject, new UpdateObjectOptions()
                        {
                            // Avoid race conditions.
                            IfMetagenerationMatch = storageObject.Metageneration,
                        });
                    }
                    //store details in a relational db including the filename/link
                    #endregion
                }

                #region Storing details of file in db 
                UsersRepository ur = new UsersRepository();     
                FilesRepository pr = new FilesRepository();

                int myId = ur.GetId(User.Identity.Name);
                p.Owner = myId;

                p.Recipient = KeyRepository.Encrypt(p.Recipient);
                p.Link = KeyRepository.Encrypt(link);

                pr.AddFile(p);
                #endregion

                #region Updating Cache with latest list of Files from db

                //enable: after you switch on db
                CacheRepository cr = new CacheRepository();
                cr.UpdateCache(pr.GetFiles(myId), myId);

                #endregion
                PubSubRepository psr = new PubSubRepository();
                psr.AddToEmailQueue(p); //adding it to queue to be sent as an email later on.
                ViewBag.Message = "File created successfully";

                new LogsRepository().WriteLogEntry("Saved");

                return RedirectToAction("");
            }
            catch (Exception ex)
            {
                new LogsRepository().LogError(ex);
            }

            return View();
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        // GET: Files
        public ActionResult Index()
        {

            #region get files of db - removed....instead insert next region
            FilesRepository pr = new FilesRepository();
            //var files = pr.GetFiles(); //gets files from db
            #endregion

            #region instead of getting files from DB, to make your application faster , you load them from the cache
            UsersRepository ur = new UsersRepository();
            

            CacheRepository cr = new CacheRepository();
            var files = cr.GetFilesFromCache(ur.GetId(User.Identity.Name));
            #endregion

            return View("Index",files);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                
                FilesRepository pr = new FilesRepository();
                pr.DeleteFile(id);
            }
            catch (Exception ex)
            {
                new LogsRepository().LogError(ex);
            }   
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteAll(int[] ids)
        {
            //1. Requirement when opening a transaction: Connection has to be opened

            FilesRepository pr = new FilesRepository();
            pr.MyConnection.Open();

            pr.MyTransaction = pr.MyConnection.BeginTransaction(); //from this point onwards all code executed against the db will remain pending

            try
            {
                foreach (int id in ids)
                {
                    pr.DeleteFile(id);
                }

                pr.MyTransaction.Commit(); //Commit: you are confirming the changes in the db
            }
            catch (Exception ex)
            {
                //Log the exception on the cloud
                new LogsRepository().LogError(ex);
                pr.MyTransaction.Rollback(); //Rollback: it will reverse all the changes done within the try-clause in the db
            }

            pr.MyConnection.Close();

            return RedirectToAction("Index");
        }
    }
}