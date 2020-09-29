package com.example.student.movies;

import android.graphics.Bitmap;

public class Movie {
    private String Title;

    public String getTitle() {
        return Title;
    }

    public void setTitle(String title) {
        Title = title;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getYear() {
        return year;
    }

    public void setYear(String year) {
        this.year = year;
    }

    public Bitmap getPoster() {
        return poster;
    }

    public void setPoster(Bitmap poster) {
        this.poster = poster;
    }

    private String id;
    private String year;
    private Bitmap poster;

    public Movie(String Title, String id, String year, Bitmap poster)
    {
        this.Title = Title;
        this.id = id;
        this.year = year;
        this.poster = poster;

    }

}
