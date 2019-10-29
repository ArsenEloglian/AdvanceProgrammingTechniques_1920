package com.example.kalkulator;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import net.objecthunter.exp4j.Expression;
import net.objecthunter.exp4j.ExpressionBuilder;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {

    private TextView resultView;
    private String result;
    private Button button0;
    private Button button1;
    private Button button2;
    private Button button3;
    private Button button4;
    private Button button5;
    private Button button6;
    private Button button7;
    private Button button8;
    private Button button9;
    private Button buttonAdd;
    private Button buttonSubtract;
    private Button buttonMultiply;
    private Button buttonDivide;
    private Button buttonEqual;
    private Button buttonDot;
    private Button buttonDel;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        init();

        buttonEqual.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(!"".equals(result)) {

                    try{
                        Expression expression = new ExpressionBuilder(result).build();
                        double calculate = expression.evaluate();
                        /*
                        Tutaj część kacpra aby zrobic funkcje przerabiajaca double na stringa

                        resultView.setText(DoubleToString(calculate));

                         */
                        result = "";

                    }
                    catch(Exception e){
                        Toast.makeText(MainActivity.this, "Incorrect", Toast.LENGTH_SHORT).show();
                    }
                }

            }
        });
    }


    private void init() {
        resultView = findViewById(R.id.textView);
        result = "";
        button0=(Button) findViewById(R.id.button0);
        button1=(Button) findViewById(R.id.button1);
        button2=(Button) findViewById(R.id.button2);
        button3=(Button) findViewById(R.id.button3);
        button4=(Button) findViewById(R.id.button4);
        button5=(Button) findViewById(R.id.button5);
        button6=(Button) findViewById(R.id.button6);
        button7=(Button) findViewById(R.id.button7);
        button8=(Button) findViewById(R.id.button8);
        button9=(Button) findViewById(R.id.button9);
        buttonAdd=(Button) findViewById(R.id.buttonPlus);
        buttonSubtract=(Button) findViewById(R.id.buttonMinus);
        buttonDivide=(Button) findViewById(R.id.buttonDivide);
        buttonMultiply=(Button) findViewById(R.id.buttonMultiply);
        buttonDel=(Button) findViewById(R.id.buttonDel);
        buttonDot=(Button) findViewById(R.id.buttonDot);
        buttonEqual=(Button) findViewById(R.id.buttonEquals);

    }

}
