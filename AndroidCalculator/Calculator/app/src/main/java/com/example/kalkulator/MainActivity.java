package com.example.kalkulator;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import net.objecthunter.exp4j.Expression;
import net.objecthunter.exp4j.ExpressionBuilder;

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
    private boolean wasCalculatedResult = false;

    private void initializeVariables() {
        resultView = findViewById(R.id.textView);
        result = "";
        button0 = (Button) findViewById(R.id.button0);
        button1 = (Button) findViewById(R.id.button1);
        button2 = (Button) findViewById(R.id.button2);
        button3 = (Button) findViewById(R.id.button3);
        button4 = (Button) findViewById(R.id.button4);
        button5 = (Button) findViewById(R.id.button5);
        button6 = (Button) findViewById(R.id.button6);
        button7 = (Button) findViewById(R.id.button7);
        button8 = (Button) findViewById(R.id.button8);
        button9 = (Button) findViewById(R.id.button9);
        buttonAdd = (Button) findViewById(R.id.buttonPlus);
        buttonSubtract = (Button) findViewById(R.id.buttonMinus);
        buttonDivide = (Button) findViewById(R.id.buttonDivide);
        buttonMultiply = (Button) findViewById(R.id.buttonMultiply);
        buttonDel = (Button) findViewById(R.id.buttonDel);
        buttonDot = (Button) findViewById(R.id.buttonDot);
        buttonEqual = (Button) findViewById(R.id.buttonEquals);
        buttonDel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                onDeleteClick();
            }
        });
        buttonDel.setOnLongClickListener(new View.OnLongClickListener() {
            @Override
            public boolean onLongClick(View v) {
                onDeleteLongClick();
                return true;
            }
        });
    }


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        initializeVariables();
        calculateResultAndDisplayOnTheScreen();
    }

    private String doubleToString(double calculate) {

        return calculate % 1 == 0 ? String.valueOf((int) calculate) : String.format ("%.3f", calculate) ;

    }

    private void calculateResultAndDisplayOnTheScreen() {
        buttonEqual.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (!"".equals(result)) {
                    try {
                        Expression expression = new ExpressionBuilder(result).build();
                        double calculate = expression.evaluate();
                        resultView.setText(doubleToString(calculate));
                        result = "";
                        wasCalculatedResult = true;
                    } catch (Exception e) {
                        Toast.makeText(MainActivity.this, "Incorrect", Toast.LENGTH_SHORT).show();
                    }
                }
            }
        });
    }

    public void onClickOtherButtons(View view) {
        Button buttonPressed = (Button) view;
        String valueFromTheButton = buttonPressed.getText().toString();
        if (!wasCalculatedResult) {
            resultView.setText(resultView.getText() + valueFromTheButton);
            result = resultView.getText().toString();
        } else {
            resultView.setText(valueFromTheButton);
            result = resultView.getText().toString();
            wasCalculatedResult = false;
        }
    }


    private void onDeleteClick() {
        result = deleteLastChar(result);
        resultView.setText(result);
    }

    private String deleteLastChar(String string) {
        if (string != null) {
            if (string.length() > 0) {
                string = string.substring(0, string.length() - 1);
            }
        }
        return string;
    }

    private void onDeleteLongClick() {
        result = "";
        resultView.setText(result);
    }
}
