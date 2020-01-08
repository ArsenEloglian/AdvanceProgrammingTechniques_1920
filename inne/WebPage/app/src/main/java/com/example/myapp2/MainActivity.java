package com.example.myapp2;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.TextView;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        TextView textView= (TextView) findViewById(R.id.wyświetl);
        try {
            textView.setText("qq0");
            URL url = new URL("http://yourwebpage.com");
            textView.setText("qq1");//openStream pada
            BufferedReader in = new BufferedReader(new InputStreamReader(url.openStream()));
            textView.setText("qq2");
            String str;
            while ((str = in.readLine()) != null) {
                textView.setText(textView.getText()+str);
            }
            in.close();
        } catch (Exception e) {
            textView.setText(textView.getText()+e.getMessage()+e.toString());
        }
    }
}
