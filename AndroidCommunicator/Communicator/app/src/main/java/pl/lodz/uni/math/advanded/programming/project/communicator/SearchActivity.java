package pl.lodz.uni.math.advanded.programming.project.communicator;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import pl.lodz.uni.math.advanded.programming.project.communicator.Friends.Friend;
import pl.lodz.uni.math.advanded.programming.project.communicator.Friends.FriendAdapter;

import android.os.Bundle;
import android.os.Debug;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.ValueEventListener;

import java.util.ArrayList;

public class SearchActivity extends AppCompatActivity {

    private FirebaseDatabase firebaseDatabase;
    private FirebaseUser firebaseUser;
    private FirebaseAuth firebaseAuth;
    private EditText searchText;
    private ListView searchList;
   // private ArrayAdapter adapter;
    private FriendAdapter friendAdapter;
   //private ArrayList<String> names;
    private  ArrayList<Friend> friendArrayList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search);

        firebaseDatabase = FirebaseDatabase.getInstance();
        firebaseAuth = FirebaseAuth.getInstance();
        firebaseUser = firebaseAuth.getCurrentUser();

        init();
        LoadUsers();
        SearchUsers();
        GoToUserProfile();

    }


    private void init() {
        searchText = findViewById(R.id.searchFilter);
        searchList = findViewById(R.id.searchList);
       // names = new ArrayList<>();
       // adapter = new ArrayAdapter(this, R.layout.list_item, names);
      //  searchList.setAdapter(adapter);
        friendArrayList = new ArrayList<Friend>();
        friendAdapter = new FriendAdapter(this,friendArrayList);
        searchList.setAdapter(friendAdapter);
    }

    // wczytanie wszystkich userow z bazy
    private void LoadUsers() {
        firebaseDatabase.getReference().child("Users").addValueEventListener(new ValueEventListener() {
            @Override
            public void onDataChange(@NonNull DataSnapshot dataSnapshot) {
                for (DataSnapshot postSnapshot : dataSnapshot.getChildren()) {
                    // TODO: handle the po
                   // names.add(postSnapshot.child("username").getValue().toString());
                    Friend friend = new Friend(postSnapshot.child("username").getValue().toString(), postSnapshot.getValue().toString());
                    friendArrayList.add(friend);
                }
            }

            @Override
            public void onCancelled(@NonNull DatabaseError databaseError) {
                // Getting Post failed, log a message

                // ...
            }
        });

    }

    // wyszukiwanie użytkowników z pobranej listy
    private void SearchUsers() {
        searchText.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
               // adapter.getFilter().filter(charSequence);
               friendAdapter.getFilter().filter(charSequence);
            }

            @Override
            public void afterTextChanged(Editable editable) {

            }
        });
    }

    // wybranie użytkownika i przejscie do jego profilu
    private void GoToUserProfile() {
        searchList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                String name = String.valueOf(adapterView.getItemAtPosition(i));
                Toast.makeText(SearchActivity.this, name, Toast.LENGTH_SHORT).show();
            }
        });
    }
}
