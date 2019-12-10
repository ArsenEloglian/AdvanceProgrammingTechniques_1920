package pl.lodz.uni.math.advanded.programming.project.communicator;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import pl.lodz.uni.math.advanded.programming.project.communicator.Friends.Friend;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.TextView;

import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.ValueEventListener;

public class ProfilActivity extends AppCompatActivity {

    private FirebaseDatabase firebaseDatabase;
    private FirebaseUser firebaseUser;
    private FirebaseAuth firebaseAuth;
    private TextView profilName;
    private Button addToFriends;
    private Button goTOChat;
    private String profileUrl;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profil);

        init();
        LoadProfil();
    }

    private void init() {
        firebaseDatabase = FirebaseDatabase.getInstance();
        firebaseAuth = FirebaseAuth.getInstance();
        firebaseUser = firebaseAuth.getCurrentUser();
        profilName = findViewById(R.id.profileName);
        addToFriends = findViewById(R.id.addToFriendsButton);
        goTOChat = findViewById(R.id.goToChatButton);

        Intent intent = getIntent();
        profileUrl = intent.getExtras().get("ProfileUrl").toString();
    }

    private void LoadProfil() {
        firebaseDatabase.getReference().child("Users").addValueEventListener(new ValueEventListener() {
            @Override
            public void onDataChange(@NonNull DataSnapshot dataSnapshot) {
                for (DataSnapshot postSnapshot : dataSnapshot.getChildren()) {
                    // TODO: handle the po
                    if (postSnapshot.getValue().toString().equals(profileUrl)) {
                        String name = postSnapshot.child("username").getValue().toString();
                        profilName.setText(name);
                    }
                }
            }

            @Override
            public void onCancelled(@NonNull DatabaseError databaseError) {
                // Getting Post failed, log a message

                // ...
            }
        });
    }
}
