package com.mcast.rod.asynctasknewsrss;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;

import java.util.List;

public class MainActivity extends AppCompatActivity {

    private final int TAG_URL = 1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        ListView listView = (ListView)findViewById(R.id.lstViewNews);
        new DownloadNewsTask(listView,this).execute("http://www.independent.com.mt/newsfeed");

        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                View rowView = view;
                String url = (String) view.getTag();

                Intent intent = new Intent(MainActivity.this, WebViewActivity.class);
                intent.putExtra("URL", url);
                startActivity(intent);

            }
        });


    }



}
