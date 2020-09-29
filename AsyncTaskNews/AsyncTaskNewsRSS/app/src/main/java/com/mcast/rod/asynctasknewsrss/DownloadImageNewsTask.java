package com.mcast.rod.asynctasknewsrss;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.widget.ImageView;

import java.io.InputStream;
import java.lang.ref.WeakReference;
import java.net.HttpURLConnection;
import java.net.URL;

/**
 * Created by vellarod on 01-Nov-16.
 */
public class DownloadImageNewsTask extends AsyncTask<String, Void,Bitmap> {

    private final WeakReference<ImageView> imageViewReference;



    public DownloadImageNewsTask(ImageView imageView) {
        // Use a WeakReference to ensure the ImageView can be garbage collected
        imageViewReference = new WeakReference<ImageView>(imageView);
    }


    @Override
    protected Bitmap doInBackground(String... params) {
        return download_Image((String)params[0]);
    }


    @Override
    protected void onPostExecute(Bitmap result) {
        if (imageViewReference != null && result != null) {
            final ImageView imageView = imageViewReference.get();
            if (imageView != null) {
                imageView.setImageBitmap(result);
            }
        }

    }


    private Bitmap download_Image(String url) {

        Bitmap bmp =null;
        try{
            URL ulrn = new URL(url);
            HttpURLConnection con = (HttpURLConnection)ulrn.openConnection();
            InputStream is = con.getInputStream();
            bmp = BitmapFactory.decodeStream(is);
            if (null != bmp)
                return bmp;

        }catch(Exception e){
            e.printStackTrace();
        }
        return bmp;
    }



}
