package com.example.app;

import androidx.appcompat.app.AppCompatActivity;

import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothServerSocket;
import android.bluetooth.BluetoothSocket;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.res.Resources;
import android.os.Bundle;
import android.os.Environment;
import android.telephony.TelephonyManager;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.ToggleButton;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.List;
import java.util.Set;
import java.util.UUID;

import static android.provider.Settings.NameValueTable.NAME;

public class MainActivity extends AppCompatActivity {
    public void wyślij(View view) {
        EditText editText = (EditText) findViewById(R.id.wiadomość);
        connectedThread.write(editText.getText().toString().getBytes());
    }

    private class ConnectedThread extends Thread {
        private final BluetoothSocket mmSocket;
        private final InputStream mmInStream;
        private final OutputStream mmOutStream;
        public ConnectedThread(BluetoothSocket socket) {
            mmSocket = socket;
            InputStream tmpIn = null;
            OutputStream tmpOut = null;
            try {
                tmpIn = socket.getInputStream();
                tmpOut = socket.getOutputStream();
            } catch (IOException e) { }
            mmInStream = tmpIn;
            mmOutStream = tmpOut;
        }
        public void run() {
            final byte[] buffer = new byte[1024];
            int bytes;
            while (true) {
                try {
                    bytes = mmInStream.read(buffer);
                    final TextView textView = (TextView) findViewById(R.id.odbiorcze);
                    textView.post(new Runnable() {
                        public void run() {
                            textView.setText(new String(buffer));
                        }
                    });
                } catch (IOException e) {
                    break;
                }
            }
        }
        public void write(byte[] bytes) {
            try {
                mmOutStream.write(bytes);
            } catch (IOException e) { }
        }
    }

    private class ConnectThread extends Thread {
        private BluetoothSocket bluetoothSocket;
        private final BluetoothDevice mmDevice;
        public ConnectThread(BluetoothDevice device) {
            BluetoothSocket tmp = null;
            mmDevice = device;
            try {
                tmp = device.createRfcommSocketToServiceRecord(UUID.fromString("00001101-0000-1000-8000-00805F9B34FB"));
            } catch (IOException e) { }
            bluetoothSocket = tmp;
        }
        public void run() {
            try {
                bluetoothSocket.connect();
            } catch (Exception connectException) {
                try {
                    bluetoothSocket.close();
                } catch (Exception closeException) { }
                acceptThread=new AcceptThread();
                acceptThread.start();
                return;
            }
            connectedThread=new ConnectedThread(bluetoothSocket);
            connectedThread.start();
        }
    }
    private class AcceptThread extends Thread {
        private BluetoothServerSocket mmServerSocket;
        BluetoothSocket bluetoothSocket;
        public AcceptThread() {
            Intent discoverableIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_DISCOVERABLE);
            discoverableIntent.putExtra(BluetoothAdapter.EXTRA_DISCOVERABLE_DURATION, 0);
            startActivity(discoverableIntent);
            BluetoothServerSocket tmp = null;
            try {
                tmp = bluetoothAdapter.listenUsingRfcommWithServiceRecord(NAME, UUID.fromString("00001101-0000-1000-8000-00805F9B34FB"));
            } catch (IOException e) { }
            mmServerSocket = tmp;
        }
        public void run() {
            while (true) {
                try {
                    bluetoothSocket = mmServerSocket.accept();
                } catch (IOException e) {
                    break;
                }
                if (bluetoothSocket != null) {
                    connectedThread=new ConnectedThread(bluetoothSocket);
                    connectedThread.start();
                    try {
                        mmServerSocket.close();
                    } catch (IOException e) {
                    }
                    break;
                }
            }
        }
    }
    private final BroadcastReceiver mReceiver = new BroadcastReceiver() {
        boolean foundDevice=false;
        public void onReceive(Context context, Intent intent) {
            String action = intent.getAction();
            if (BluetoothDevice.ACTION_FOUND.equals(action)) {
                BluetoothDevice device = intent.getParcelableExtra(BluetoothDevice.EXTRA_DEVICE);
                if(device.getBondState()==10){//10-ńe sojedinione,12-zojedinione
                    try {
                        Method createBond = device.getClass().getMethod("createBond");
                        Object object= createBond.invoke(device);
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
                String deviceName=device.getName();
                if(deviceName!=null&&deviceName.length()==21){
                    foundDevice=true;
                    adapter.add(device.getName() + " connect: " + device.getAddress());
                    adapter.notifyDataSetChanged();
                    connectThread=new ConnectThread(device);
                    connectThread.start();
                    bluetoothAdapter.cancelDiscovery();
                }else{
                    adapter.add(device.getName() + " noConnect: " + device.getAddress());
                    adapter.notifyDataSetChanged();
                }
            }else if(BluetoothAdapter.ACTION_DISCOVERY_FINISHED.equals(action)){
                if(!foundDevice){
                    acceptThread=new AcceptThread();
                    acceptThread.start();
                }
            }else if(BluetoothDevice.ACTION_ACL_CONNECTED.equals(action)){
                adapter.add("ACL_CONNECTED");
                adapter.notifyDataSetChanged();
            }else if(BluetoothDevice.ACTION_ACL_DISCONNECTED.equals(action)){
                adapter.add("ACL_DISCONNECTED");
                adapter.notifyDataSetChanged();
            }
        }
    };
    AcceptThread acceptThread;
    ConnectThread connectThread;
    ConnectedThread connectedThread;
    BluetoothAdapter bluetoothAdapter;
    ArrayAdapter<String> adapter;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        setTitle("androidCommunicatorXi");
        {//twórz adapter
            Spinner spinner1 = (Spinner) findViewById(R.id.spinnerZdarzeńa);
            List<String> spinnerArray = new ArrayList<>();
            adapter= new ArrayAdapter<String>(this,android.R.layout.simple_spinner_item,spinnerArray);
            spinner1.setAdapter(adapter);
        }
        {//włącz niebieski
            bluetoothAdapter= BluetoothAdapter.getDefaultAdapter();
            bluetoothAdapter.setName("androidCommunicatorXi");
            startActivityForResult(new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE), 0);
            adapter.add("<<"+bluetoothAdapter.getAddress().toString());
            adapter.notifyDataSetChanged();
            try {
                Method method = BluetoothAdapter.class.getMethod("setScanMode", int.class);
                method.invoke(bluetoothAdapter, BluetoothAdapter.SCAN_MODE_CONNECTABLE);
            } catch (NoSuchMethodException | IllegalArgumentException | IllegalAccessException | InvocationTargetException e) {}
            IntentFilter filter = new IntentFilter(BluetoothDevice.ACTION_FOUND);
            filter.addAction(BluetoothAdapter.ACTION_DISCOVERY_FINISHED);
            filter.addAction(BluetoothDevice.ACTION_ACL_CONNECTED);
            filter.addAction(BluetoothDevice.ACTION_ACL_DISCONNECTED);
            registerReceiver(mReceiver, filter);
        }
        Set<BluetoothDevice> pairedDevices= bluetoothAdapter.getBondedDevices();
        boolean foundPairedDevice=false;
        for(BluetoothDevice bt : pairedDevices) if(bt.getName().length()==21){
            foundPairedDevice=true;
            connectThread=new ConnectThread(bt);
            connectThread.start();
        };
        if(!foundPairedDevice){
            bluetoothAdapter.startDiscovery();
        }
    }
    @Override
    protected void onDestroy() {
        super.onDestroy();
        unregisterReceiver(mReceiver);
    }
}
