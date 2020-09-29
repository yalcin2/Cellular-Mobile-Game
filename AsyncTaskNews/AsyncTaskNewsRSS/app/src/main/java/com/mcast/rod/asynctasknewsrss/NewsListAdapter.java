package com.mcast.rod.asynctasknewsrss;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import java.util.List;

/**
 * Created by roderickv on 1-11-16.
 */
public class NewsListAdapter extends ArrayAdapter {

    private  int resource;
    private LayoutInflater inflater;
    private Context context;

    private final int TAG_URL = 1;

    public  NewsListAdapter(Context cx, int resourceId, List<News> objects)
    {
        super( cx, resourceId, objects );
        resource = resourceId;
        inflater = LayoutInflater.from(cx);
        context = cx;
    }


    @Override
    public View getView (int position, View convertView, ViewGroup parent ) {

        /* create a new view of my layout and inflate it in the row */
        convertView = (RelativeLayout) inflater.inflate( resource, null );


        News news = (News) getItem( position );


        TextView txtTitle = (TextView) convertView.findViewById(R.id.templateTitle);
        txtTitle.setText(news.getTitle());

        ImageView imageView = (ImageView)convertView.findViewById(R.id.templateThumbImage);
        new DownloadImageNewsTask(imageView).execute(news.getImageURL());

        convertView.setTag(news.getNewsURL());

        return convertView;

    }



}
