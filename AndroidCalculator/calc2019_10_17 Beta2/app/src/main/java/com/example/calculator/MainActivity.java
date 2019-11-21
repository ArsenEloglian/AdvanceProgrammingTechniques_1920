package com.example.calculator;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

/*import net.*;
import net.objecthunter.exp4j.*;
import net.objecthunter.exp4j.Expression;
import net.objecthunter.exp4j.ExpressionBuilder;
import net;*/

public class MainActivity extends AppCompatActivity {
    private Button buttonC;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        buttonC = (Button) findViewById(R.id.buttonC);
        buttonC.setOnLongClickListener(new View.OnLongClickListener() {
            @Override
            public boolean onLongClick(View v) {
                onDeleteLongClick();
                return true;
            }
        });
    }

    private void onDeleteLongClick() {
        TextView tv = findViewById(R.id.textView);
        tv.setText("");
    }

    public void handleClick(View view) {
        Button btn = (Button)view;
        TextView tv = findViewById(R.id.textView);
        tv.setText(tv.getText()+""+btn.getText());
    }
    private String deleteLastChar(String string) {
        if (string != null) {
            if (string.length() > 0) {
                string = string.substring(0, string.length() - 1);
            }
        }
        return string;
    }
    public void handleClickC(View view) {
        TextView tv = findViewById(R.id.textView);
        tv.setText(deleteLastChar((String) tv.getText()));
    }

    public void handleClickWynik(View view) {
        TextView tv = findViewById(R.id.textView);
        if (!"".equals(tv.getText())) {
            try {
/*                Expression expression = new ExpressionBuilder(result).build();
                double calculate = expression.evaluate();
                resultView.setText(doubleToString(calculate));
                result = "";
                wasCalculatedResult = true;*/
            } catch (Exception e) {
                Toast.makeText(MainActivity.this, "Incorrect", Toast.LENGTH_SHORT).show();
            }
        }
    }
}




