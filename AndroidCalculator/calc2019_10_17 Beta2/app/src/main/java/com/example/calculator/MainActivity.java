package com.example.calculator;

<<<<<<< HEAD
=======
import androidx.appcompat.app.AppCompatActivity;

>>>>>>> 87ec5e1d35f88f320fabe0d3c4f7701bae3844c7
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
<<<<<<< HEAD
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

/*import net.*;
import net.objecthunter.exp4j.*;
import net.objecthunter.exp4j.Expression;
import net.objecthunter.exp4j.ExpressionBuilder;
import net;*/

public class MainActivity extends AppCompatActivity {
    private Button buttonC;
=======

public class MainActivity extends AppCompatActivity {

>>>>>>> 87ec5e1d35f88f320fabe0d3c4f7701bae3844c7
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
<<<<<<< HEAD
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
=======
		findViewById(R.id.button0).setOnClickListener(handleClick);
        findViewById(R.id.button1).setOnClickListener(handleClick);
        findViewById(R.id.button2).setOnClickListener(handleClick);
        findViewById(R.id.button3).setOnClickListener(handleClick);
        findViewById(R.id.button4).setOnClickListener(handleClick);
        findViewById(R.id.button5).setOnClickListener(handleClick);
        findViewById(R.id.button6).setOnClickListener(handleClick);
        findViewById(R.id.button7).setOnClickListener(handleClick);
        findViewById(R.id.button8).setOnClickListener(handleClick);
        findViewById(R.id.button9).setOnClickListener(handleClick);
        findViewById(R.id.buttonOdejmij).setOnClickListener(operClick);
        findViewById(R.id.buttonDziel).setOnClickListener(operClick);
        findViewById(R.id.buttonDodaj).setOnClickListener(operClick);
        findViewById(R.id.buttonMnoz).setOnClickListener(operClick);
        findViewById(R.id.buttonWynik).setOnClickListener(eqClick);
    }
    int which=0;
    int first=0;
    int second=0;
    char what;
    public View.OnClickListener handleClick = new View.OnClickListener(){
        public void onClick(View arg0) {
            Button btn = (Button)arg0;
            TextView tv = findViewById(R.id.textView);
            tv.setText(tv.getText()+""+btn.getText());
            if(which==0){
                first*=10;
                first+=Integer.parseInt(btn.getText().toString());
            }
            if(which==1){
                second*=10;
                second+=Integer.parseInt(btn.getText().toString());
            }
        }
    };
    public View.OnClickListener operClick = new View.OnClickListener(){
        public void onClick(View arg0) {
            if(which==0){
                which++;
                Button btn = (Button)arg0;
                TextView tv = findViewById(R.id.textView);
                what=btn.getText().charAt(0);
                tv.setText(tv.getText()+""+what);
            }
        }
    };
    public View.OnClickListener eqClick = new View.OnClickListener(){
        public void onClick(View arg0) {
            if(which==1&&second!=0){
                which=0;
                TextView tv = findViewById(R.id.textView);
                if(what=='+'){
                    first+=second;
                }else if(what=='-'){
                    first-=second;
                }else if(what=='*'){
                    first*=second;
                }else if(what=='/'){
                    first/=second;
                };
                second=0;
                tv.setText(Integer.toString(first));
            }
        }
    };
>>>>>>> 87ec5e1d35f88f320fabe0d3c4f7701bae3844c7
}




