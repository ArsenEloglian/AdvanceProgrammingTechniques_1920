package com.example.app00;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.Manifest;
import android.annotation.SuppressLint;
import android.app.PendingIntent;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.hardware.Camera;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Build;
import android.os.Bundle;
import android.net.wifi.WifiManager;
import android.content.Context;
import android.os.VibrationEffect;
import android.os.Vibrator;
import android.provider.MediaStore;
import android.telephony.SmsManager;
import android.telephony.TelephonyManager;
import android.telephony.gsm.GsmCellLocation;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;

import java.util.List;

public class MainActivity extends AppCompatActivity implements SensorEventListener {

    Button enableButton, disableButton;
    EditText eText1, eText2, eText3, eText4;
    //CameraView cv;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
      //  view_camera.bindToLifecycle(this);
        SensorManager mSensorManager = (SensorManager) getApplicationContext().getSystemService(Context.SENSOR_SERVICE);
        mSensorManager.registerListener(this, mSensorManager.getDefaultSensor(Sensor.TYPE_PROXIMITY), SensorManager.SENSOR_DELAY_NORMAL);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        enableButton = (Button) findViewById(R.id.button1);
        disableButton = (Button) findViewById(R.id.button2);
        eText1 = (EditText) findViewById(R.id.editText1);
        eText2 = (EditText) findViewById(R.id.editText2);
        eText3 = (EditText) findViewById(R.id.editText3);
        eText4 = (EditText) findViewById(R.id.editText4);

        enableButton.setOnClickListener(new OnClickListener() {
            @RequiresApi(api = Build.VERSION_CODES.O)
            public void onClick(final View v) {
                WifiManager wifi = (WifiManager) getApplicationContext().getSystemService(Context.WIFI_SERVICE);
                wifi.setWifiEnabled(true);
                SensorManager mSensorManager = (SensorManager) getApplicationContext().getSystemService(Context.SENSOR_SERVICE);
                List<Sensor> sensorList = mSensorManager.getSensorList(Sensor.TYPE_ALL);
                for (Sensor snsr : sensorList) {
                    //eText.setText(eText.getText()+"NAME-"+snsr.getName()+":POWER-"+String.valueOf(snsr.getPower())+":VENDOR-"+snsr.getVendor()+":VERSION-"+String.valueOf(snsr.getVersion())+":TYPEint-"+String.valueOf(snsr.getType())+":TYPE-"+snsr.getStringType()+"\r\n");
                }
                mSensorManager.registerListener(new SensorEventListener() {
                    long lastUpdate = System.currentTimeMillis();
                    float last_x = 0, last_y = 0, last_z = 0;

                    @Override
                    public void onSensorChanged(SensorEvent event) {
                        Sensor mySensor = event.sensor;
                        if (mySensor.getType() == Sensor.TYPE_ACCELEROMETER) {
                            long curTime = System.currentTimeMillis();
                            if ((curTime - lastUpdate) > 100) {
                                long diffTime = (curTime - lastUpdate);
                                lastUpdate = curTime;
                                float x = event.values[0];
                                float y = event.values[1];
                                float z = event.values[2];
                                float speed = Math.abs(x + y + z - last_x - last_y - last_z) / diffTime * 10000;
                                if (speed > 1000) {
                                    Vibrator vib = (Vibrator) getApplicationContext().getSystemService(Context.VIBRATOR_SERVICE);
                                    vib.vibrate(100);
                                }
                                last_x = x;
                                last_y = y;
                                last_z = z;
                                eText3.setText(String.valueOf(speed));
                            }
                        }
                    }

                    @Override
                    public void onAccuracyChanged(Sensor sensor, int accuracy) {
                    }
                }, mSensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER), SensorManager.SENSOR_DELAY_NORMAL);
                //mSensorManager.registerListener(this, sensor, SensorManager.SENSOR_DELAY_NORMAL);
                //long[] pattern = {0, 100, 1000};
                //vib.vibrate(pattern, 0);
               /*
                WifiInfo info = wifi.getConnectionInfo();
                String macAddress=info.getMacAddress();*/
                TelephonyManager telephony = (TelephonyManager) getApplicationContext().getSystemService(Context.TELEPHONY_SERVICE);
                //telephony.setDataEnabled(true);
                Vibrator vib = (Vibrator) getApplicationContext().getSystemService(Context.VIBRATOR_SERVICE);
                vib.vibrate(700);
/*                @SuppressLint("MissingPermission") String telephonyId = telephony.getDeviceId();
                enableButton.setText(macAddress);
                disableButton.setText(telephonyId);
                Vibrator vib = (Vibrator) getApplicationContext().getSystemService(Context.VIBRATOR_SERVICE);
                vib.vibrate(5000);*/
            }
        });
        disableButton.setOnClickListener(new OnClickListener() {
            @RequiresApi(api = Build.VERSION_CODES.M)
            public void onClick(View v) {
                WifiManager wifi = (WifiManager) getApplicationContext().getSystemService(Context.WIFI_SERVICE);
                wifi.setWifiEnabled(false);
/*                TelephonyManager telephony = (TelephonyManager) getApplicationContext().getSystemService(Context.TELEPHONY_SERVICE);
                if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
                    //telephony.setDataEnabled(false);
                }
                //Vibrator vib = (Vibrator) getApplicationContext().getSystemService(Context.VIBRATOR_SERVICE);
                //vib.vibrate(700);
                //eText1.setText("DeviceId"+telephonyId);
                //SmsManager sms = SmsManager.getDefault();
                if (checkSelfPermission(Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && checkSelfPermission(Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
                    // TODO: Consider calling
                    //    Activity#requestPermissions
                    // here to request the missing permissions, and then overriding
                    //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
                    //                                          int[] grantResults)
                    // to handle the case where the user grants the permission. See the documentation
                    // for Activity#requestPermissions for more details.
                    eText2.setText("błąd");
                    return;
                }
                GsmCellLocation loc = (GsmCellLocation) telephony.getCellLocation();
                eText2.setText("cellId:"+String.valueOf(loc.getCid()));
                eText3.setText("LocationAreaCode:"+String.valueOf(loc.getLac()));
                //eText2.setText("cellId:"+String.valueOf(loc.getCid()));
                //PendingIntent sentIntent = PendingIntent.getActivity(this, 0, new Intent(this,SmsSendCheck.class), 0);
                //Vibrator vib = (Vibrator) getApplicationContext().getSystemService(Context.VIBRATOR_SERVICE);
                //vib.vibrate(200);
                //WifiManager wifi = (WifiManager) getApplicationContext().getSystemService(Context.WIFI_SERVICE);
                //wifi.setWifiEnabled(false);*/
            }
        });
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        //getMenuInflater().inflate(R.menu.activity_main, menu);
        return true;
    }

    @Override
    public void onSensorChanged(SensorEvent event) {
        eText4.setText(String.valueOf(event.values[0]));
        if(event.values[0]==0){
            Vibrator vib = (Vibrator) getApplicationContext().getSystemService(Context.VIBRATOR_SERVICE);
            vib.vibrate(100);
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {

    }
}
