package com.example.student.movies;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {

    ListView movieListView; //Declaring a list view
    ArrayList<Movie> movies = null;
    TextView txtMovie;
    Button btnSearch;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        txtMovie = findViewById(R.id.txt_Search);
        btnSearch = findViewById(R.id.btn_search);

        movieListView = findViewById(R.id.lst_Movies); //Initializing the list view found on the activity_main

        //new MovieTask(this).execute("bean");
    }

    public void Search(View view){
        String textToSearch = txtMovie.getText().toString();
        new MovieTask(this).execute(textToSearch);
    }


    public class MovieTask extends AsyncTask<String, Void, List<Movie>>{

        final String url = "http://www.omdbapi.com/?apikey=4e5b1c57&s=";
        Context context;

        public MovieTask(Context context){
            this.context = context;
        }

        @Override
        protected List<Movie> doInBackground(String[] strings) {
            String movieSearchUrl = url + strings[0];
            StringBuilder result = new StringBuilder();
            HttpURLConnection urlConnection = null;
            ArrayList<Movie> movieArrayList= new ArrayList<>();

            try{
                URL url = new URL(movieSearchUrl);
                urlConnection = (HttpURLConnection)url.openConnection();
                InputStream inputStream = new BufferedInputStream(urlConnection.getInputStream());

                BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream));

                String line;
                while((line = reader.readLine()) != null)
                {
                    result.append(line);
                }

                JSONObject movieObject = null;

                try{
                    movieObject = new JSONObject((result.toString()));

                    JSONArray movieList = movieObject.getJSONArray("Search");//You are calling the array containing the object called Search
                    for(int i=0; i<movieList.length(); i++)
                    {
                        JSONObject m = movieList.getJSONObject(i);

                        Bitmap bmp = null;
                        try{
                            bmp = BitmapFactory.decodeStream(new URL(m.getString("Poster")).openConnection().getInputStream()); //You are converting the poster url into bitmap
                        }
                        catch (IOException e)
                        {
                            e.printStackTrace();
                        }

                        movieArrayList.add(new Movie(m.getString("Title"), m.getString("imdbID"), m.getString("Year"), bmp)); //Caling the different attributes of an object from the JSON file

                    }
                }
                catch (JSONException e){
                    e.printStackTrace();
                }

            }
            catch (Exception ex){
                ex.printStackTrace();
            }
            finally{
                urlConnection.disconnect();
            }

            return movieArrayList;
        }

        @Override
        protected  void onPostExecute(List<Movie> mList)
        {
            //databinding to listviw
            ListView listview = findViewById(R.id.lst_Movies);
            listview.setAdapter(new MovieListAdapter(context, R.layout.listviewtemplate, mList));
        }
    }


}
