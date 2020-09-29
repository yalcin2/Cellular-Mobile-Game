package com.example.student.movies;

import android.content.Context;
import android.content.Intent;
import android.media.Image;
import android.net.Uri;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.List;

public class MovieListAdapter extends ArrayAdapter {

    private int resource;
    private LayoutInflater inflater;
    private Context context;

    public MovieListAdapter(Context cx, int resourceId, List<Movie> objects ){
        super(cx, resourceId, objects);

        this.resource = resourceId;
        this.context = cx;
        inflater = LayoutInflater.from(cx);
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent)
    {
        if(convertView == null)
        {
            convertView = inflater.inflate(resource, null); //converts the listviewtemplate into convertView
        }

        TextView txtTitle = convertView.findViewById(R.id.txt_title);
        TextView txtYear = convertView.findViewById(R.id.txt_Year);
        ImageView imgMovie = convertView.findViewById(R.id.imgPost);

        final Movie objMovie = (Movie)getItem(position);

        txtTitle.setText(objMovie.getTitle());
        txtYear.setText(objMovie.getYear());
        imgMovie.setImageBitmap(objMovie.getPoster());

        //setting a onclick listener when the user clickes on a movie
        convertView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String link = "http://www.imdb.com/title/" + objMovie.getId();
                Intent browserIntent = new Intent(Intent.ACTION_VIEW);
                browserIntent.setData(Uri.parse(link));
                context.startActivity(browserIntent);
            }
        });

        return convertView;
    }
}
