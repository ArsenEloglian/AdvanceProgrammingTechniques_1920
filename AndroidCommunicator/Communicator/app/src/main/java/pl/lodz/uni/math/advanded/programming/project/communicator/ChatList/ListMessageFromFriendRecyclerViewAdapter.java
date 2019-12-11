package pl.lodz.uni.math.advanded.programming.project.communicator.ChatList;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.ValueEventListener;
import com.squareup.picasso.Picasso;

import java.util.ArrayList;
import java.util.List;

import pl.lodz.uni.math.advanded.programming.project.communicator.Chat.ChatActivity;
import pl.lodz.uni.math.advanded.programming.project.communicator.R;
import pl.lodz.uni.math.advanded.programming.project.communicator.User;

public class ListMessageFromFriendRecyclerViewAdapter extends RecyclerView.Adapter<ListMessageFromFriendRecyclerViewAdapter.ItemViewHolder> {
    private List<User> chatFriendList = new ArrayList<>();
    private Context mContext;
    private DatabaseReference databaseReference;


    public ListMessageFromFriendRecyclerViewAdapter(Context mContext, DatabaseReference ref, final ArrayList<String> chatFriendsIdList) {
        this.mContext = mContext;
        this.databaseReference = ref;
        databaseReference.addValueEventListener(new ValueEventListener() {
            @Override
            public void onDataChange(@NonNull DataSnapshot dataSnapshot) {
                chatFriendList.clear();
                for (DataSnapshot postSnapshot : dataSnapshot.getChildren()) {
                    for (int i = 0; i < chatFriendsIdList.size(); i++) {
                        if (postSnapshot.getKey().equals(chatFriendsIdList.get(i))) {
                            User friend = new User();
                            String name = postSnapshot.child("username").getValue().toString();
                            String photo = postSnapshot.child("imageUrl").getValue().toString();
                            String id = postSnapshot.getKey();
                            friend.setUsername(name);
                            friend.setImageUrl(photo);
                            friend.setId(id);
                            chatFriendList.add(friend);
                        }
                    }
                }
                notifyDataSetChanged();
            }

            @Override
            public void onCancelled(@NonNull DatabaseError databaseError) {

            }
        });
    }

    @NonNull
    @Override
    public ItemViewHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
        View view;
        LayoutInflater mInflater = LayoutInflater.from(mContext);
        view = mInflater.inflate(R.layout.user_layout, viewGroup, false);
        return new ItemViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull final ItemViewHolder itemViewHolder, final int i) {
        itemViewHolder.myName.setText(chatFriendList.get(i).getUsername());
        Picasso.with(mContext).load(chatFriendList.get(i).getImageUrl()).placeholder(R.drawable.ic_launcher_foreground).into(itemViewHolder.myImage);
        itemViewHolder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String id = chatFriendList.get(i).getId();
                Intent intent = new Intent(mContext, ChatActivity.class);
                intent.putExtra("FRIEND_ID", id);
                mContext.startActivity(intent);
            }
        });
        itemViewHolder.itemView.setOnLongClickListener(new View.OnLongClickListener() {
            @Override
            public boolean onLongClick(View v) {
                String id = chatFriendList.get(i).getId();
/*                Intent intent = new Intent(mContext, ProfileActivity.class);
                intent.putExtra(StaticVariables.KEY_FRIEND_ID, id);
                mContext.startActivity(intent);*/
                return true;
            }
        });
    }

    @Override
    public int getItemCount() {
        return chatFriendList.size();
    }

    public class ItemViewHolder extends RecyclerView.ViewHolder {
        TextView myName;
        ImageView myImage;

        public ItemViewHolder(@NonNull View itemView) {
            super(itemView);
            myName = itemView.findViewById(R.id.txtFriendName);
            myImage = itemView.findViewById(R.id.profileFriendPhoto);
        }

    }
}
