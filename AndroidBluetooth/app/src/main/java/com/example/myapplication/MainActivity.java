package com.example.myapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothServerSocket;
import android.bluetooth.BluetoothSocket;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Set;
import java.util.UUID;

import static android.provider.Settings.NameValueTable.NAME;

public class MainActivity extends AppCompatActivity {
    private class ServerThread extends Thread {
        private BluetoothServerSocket bluetoothServerSocket=null;
        public ServerThread() {
            try {
                bluetoothServerSocket = BA.listenUsingRfcommWithServiceRecord(NAME, UUID.fromString("fa87c0d0-afac-11de-8a39-0800200c9a66"));
            } catch (IOException e) {
            }
        }
        BluetoothSocket bluetoothSocket = null;
        public void run() {
            if(bluetoothServerSocket!=null&&bluetoothSocket==null){
                try {
                    bluetoothSocket = bluetoothServerSocket.accept();
                    TextView textView = (TextView)findViewById(R.id.powiadomienia);
                    textView.setText("połączono");
                    //mConnectedThread = new ConnectedThread(socket);
                    //mConnectedThread.start();
                } catch (IOException e) {
                }
            }
        }
    }
    BluetoothAdapter BA= BluetoothAdapter.getDefaultAdapter();
    BluetoothDevice bluetoothDevice;
    ServerThread serverThread;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        BA.setName("androidCommunicatorXi");
        startActivityForResult(new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE), 0);
        Set<BluetoothDevice> pairedDevices= BA.getBondedDevices();
        BluetoothDevice bluetoothDevice=pairedDevices.iterator().next();
        TextView textView = (TextView)findViewById(R.id.powiadomienia);
        textView.setText("attessamy");
        //serverThread = new ServerThread();
        //serverThread.start();

        BluetoothSocket bluetoothSocket;
        try {
            bluetoothSocket = bluetoothDevice.createRfcommSocketToServiceRecord(UUID.fromString("fa87c0d0-afac-11de-8a39-0800200c9a66"));
            bluetoothSocket.connect();
        } catch (IOException e) {
            TextView textView2 = (TextView)findViewById(R.id.powiadomienia);
            textView2.setText("błąd połączeńa");
        }

        ListView lv = (ListView)findViewById(R.id.listView);
        ArrayList list = new ArrayList();
        for(BluetoothDevice bt : pairedDevices) list.add(Integer.toString(bt.getName().toString().length()));
        lv.setAdapter(new ArrayAdapter(this,android.R.layout.simple_list_item_1, list));

        if(bluetoothDevice==null){
            setTitle("? androidCommunicatorXi ?");
            return;
        }
        setTitle("androidCommunicatorXi");

        //startActivityForResult(new Intent(BluetoothAdapter.ACTION_REQUEST_DISCOVERABLE), 0);


    }
    private BluetoothSocket bluetoothSocket=null;
    public void wyślij(View view) {
/*
        if(pairedDevices.size()!=0){
            BluetoothDevice bluetoothDevice=pairedDevices.iterator().next();
            try {
                bluetoothSocket = bluetoothDevice.createRfcommSocketToServiceRecord(UUID.fromString("fa87c0d0-afac-11de-8a39-0800200c9a66"));
                bluetoothSocket.connect();
            } catch (IOException e) {
            }

            mmSocket = tmp;
        }*/
    }
}
