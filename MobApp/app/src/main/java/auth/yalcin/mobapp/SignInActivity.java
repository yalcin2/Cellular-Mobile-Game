package auth.yalcin.mobapp;

import android.media.Image;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.TextView;

import com.facebook.FacebookActivity;

import java.util.Arrays;

public class SignInActivity extends AppCompatActivity {

    ImageButton imageButton1;
    ImageButton imageButton2;
    ImageButton imageButton3;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_in);
        addListenerOnButton1();
        addListenerOnButton2();
        addListenerOnButton3();
    }

    public void addListenerOnButton1() {

        final Context context = this;

        imageButton1 = (ImageButton) findViewById(R.id.imageButton1);

        imageButton1.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View arg0) {
                Intent intent = new Intent(context, GoogleSignInActivity.class);
                startActivity(intent);

            }

        });
    }

    public void addListenerOnButton2() {

        final Context context = this;

        imageButton2 = (ImageButton) findViewById(R.id.imageButton2);

        imageButton2.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View arg0) {
                Intent intent = new Intent(context, EmailPasswordActivity.class);
                startActivity(intent);

            }

        });
    }

    public void addListenerOnButton3() {

        final Context context = this;

        imageButton3 = (ImageButton) findViewById(R.id.imageButton3);

        imageButton3.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View arg0) {
                Intent intent = new Intent(context, FacebookLoginActivity.class);
                startActivity(intent);

            }

        });
    }

}