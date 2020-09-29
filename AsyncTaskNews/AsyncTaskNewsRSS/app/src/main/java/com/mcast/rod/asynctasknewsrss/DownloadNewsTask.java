package com.mcast.rod.asynctasknewsrss;

import android.content.Context;
import android.os.AsyncTask;
import android.os.Debug;
import android.util.Log;
import android.widget.ListView;

import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserFactory;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.lang.ref.WeakReference;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by roderickv on 1-11-16.
 */
public class DownloadNewsTask extends AsyncTask<String, Void,List<News>> {

    private final WeakReference<ListView> listViewReference;
    private Context context;


    public DownloadNewsTask(ListView listView, Context context){

        listViewReference = new WeakReference<ListView>(listView);
        this.context = context;
    }

    //http://stackoverflow.com/questions/17434135/how-to-parse-an-rss-feed-with-xmlpullparser
    @Override
    protected List<News> doInBackground(String... params) {

        String fullString = "";
        URL url = null;
        List<News> newsList = new ArrayList<>();
        try {
            //get url
            url = new URL(params[0]);

            XmlPullParserFactory factory = XmlPullParserFactory.newInstance();
            factory.setNamespaceAware(false);
            XmlPullParser xpp = factory.newPullParser();
            xpp.setInput(url.openConnection().getInputStream(),null);


            boolean insideItem = false;

            News objnews = null;

            int eventType = xpp.getEventType();
            while (eventType != XmlPullParser.END_DOCUMENT) {
                if (eventType == XmlPullParser.START_TAG) {

                    if (xpp.getName().equalsIgnoreCase("item")) {
                        insideItem = true;
                        objnews = new News();
                    } else if (xpp.getName().equalsIgnoreCase("title")) {
                        if (insideItem)
                            //Log.i("....",xpp.nextText()); // extract the headline
                            objnews.setTitle(xpp.nextText());
                    } else if (xpp.getName().equalsIgnoreCase("link")) {
                        if (insideItem)
                            //Log.i("....",xpp.nextText());  // extract the link of article
                            objnews.setNewsURL(xpp.nextText());
                    } else  if(xpp.getName().equalsIgnoreCase("description")){
                        if (insideItem)
                            objnews.setDescription(xpp.nextText());
                    } else  if(xpp.getName().equalsIgnoreCase("enclosure")){
                        if(insideItem)
                            objnews.setImageURL(xpp.getAttributeValue(null,"url") + "&width=160&height=145");
                    }

                } else if (eventType == XmlPullParser.END_TAG && xpp.getName().equalsIgnoreCase("item")) {
                    insideItem = false;
                    newsList.add(objnews);
                }

                eventType = xpp.next(); // move to next element
            }


        } catch (Exception e) {
            e.printStackTrace();
        }

        return  newsList;
    }


    @Override
    protected void onPostExecute(List<News> result) {
        if (listViewReference != null && result != null) {
            final ListView listView = listViewReference.get();
            if (listView != null) {
                listView.setAdapter(new NewsListAdapter(context,R.layout.listviewtemplate,result));
            }
        }
    }



}
