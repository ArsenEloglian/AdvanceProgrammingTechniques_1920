package pl.lodz.uni.math.advanded.programming.project.communicator;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.Button;
import android.widget.TextView;

public class ProfilActivity extends AppCompatActivity {

    private TextView profilName;
    private Button addToFriends;
    private Button goTOChat;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profil);

        init();
    }

    private void init()
    {
        profilName = findViewById(R.id.profileName);
        addToFriends = findViewById(R.id.addToFriendsButton);
        goTOChat = findViewById(R.id.goToChatButton);
    }
}
