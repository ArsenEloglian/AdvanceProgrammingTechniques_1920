package pl.lodz.uni.math.advanded.programming.project.communicator.Chat;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

import pl.lodz.uni.math.advanded.programming.project.communicator.R;

public class ChatActivity extends AppCompatActivity {
    private RecyclerView messagesListRecyclerView;
    private ChatRecyclerViewAdapter chatRecyclerViewAdapter;
    private Button sendMessageButton;
    private DatabaseReference friendsOtherUserReference;
    private DatabaseReference databaseReference;
    private FirebaseAuth firebaseAuth;
    private String userId;
    private String friendUserId = "KczgYypfejcYEQcuNsPneuQrUDp2";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_chat);
        initializeVariables();
        setOnCLickActionOnSendMessageButton();
        userId = getUserIdFromDatabase();
        friendUserId = getIntent().getStringExtra("FRIEND_ID");
        Log.e("USER_ID", userId);
        Log.e("FRIEND_ID", friendUserId);
        displayChatMessage(userId, friendUserId);
    }

    private void initializeVariables() {
        sendMessageButton = findViewById(R.id.send_message_button);
        messagesListRecyclerView = findViewById(R.id.list_of_message);
    }

    private String getUserIdFromDatabase() {
        firebaseAuth = FirebaseAuth.getInstance();
        final FirebaseUser user = firebaseAuth.getCurrentUser();
        if (user != null) {
            return user.getUid();
        } else {
            return null;
        }
    }

    private void setOnCLickActionOnSendMessageButton() {
        sendMessageButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                EditText input = findViewById(R.id.message_input);
                FirebaseDatabase.getInstance().getReference("Users").child(userId).child("chat").child(friendUserId).push().setValue(new ChatMessage(input.getText().toString(),
                        FirebaseAuth.getInstance().getCurrentUser().getEmail()));

                FirebaseDatabase.getInstance().getReference("Users").child(friendUserId).child("chat").child(userId).push().setValue(new ChatMessage(input.getText().toString(),
                        FirebaseAuth.getInstance().getCurrentUser().getEmail()));
                Log.e("USER_MESSAGE", input.getText().toString() + " " + FirebaseAuth.getInstance().getCurrentUser().getEmail());
                input.setText("");
            }
        });
    }

    private void displayChatMessage(String userId, String friendUserId) {
        databaseReference = FirebaseDatabase.getInstance().getReference("Users").child(userId);
        friendsOtherUserReference = databaseReference.child("chat").child(friendUserId);
        Log.e("CHAT_PATH", "Users/" + userId + "/chat" + friendUserId);
        chatRecyclerViewAdapter = new ChatRecyclerViewAdapter(ChatActivity.this, friendsOtherUserReference);
        messagesListRecyclerView.setLayoutManager(new LinearLayoutManager(this));
        messagesListRecyclerView.setHasFixedSize(true);
        messagesListRecyclerView.setAdapter(chatRecyclerViewAdapter);
        chatRecyclerViewAdapter.notifyDataSetChanged();
    }
}
