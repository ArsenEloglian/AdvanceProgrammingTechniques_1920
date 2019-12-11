package pl.lodz.uni.math.advanded.programming.project.communicator.ChatList;

import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

import java.util.ArrayList;

import pl.lodz.uni.math.advanded.programming.project.communicator.R;


public class ListMessageFromFriendsFragment extends Fragment {

    private RecyclerView listMessageFromFriendRecyclerView;
    private DatabaseReference databaseReference;
    public static ArrayList<String> chatFriendsIdList;
    private ListMessageFromFriendRecyclerViewAdapter listMessageFromFriendRecyclerViewAdapter;
    private View view;
    private FirebaseAuth firebaseAuth;


    public ListMessageFromFriendsFragment() {
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        view = inflater.inflate(R.layout.fragment_list_message_from_friends, container, false);
        listMessageFromFriendRecyclerView = (RecyclerView) view.findViewById(R.id.chatListFriendsListRecyclerView);


        //chatFriendsIdList = (ArrayList<String>) getActivity().getIntent().getSerializableExtra("CHAT_FRIEND_ID_LIST");
        chatFriendsIdList = new ArrayList<>();
        chatFriendsIdList.add("-Lu7p72puQ3EdQ5K0IUq");
        chatFriendsIdList.add("KczgYypfejcYEQcuNsPneuQrUDp2");
        if (!chatFriendsIdList.isEmpty()) {
            databaseReference = FirebaseDatabase.getInstance().getReference().child("Users");
            listMessageFromFriendRecyclerViewAdapter = new ListMessageFromFriendRecyclerViewAdapter(getActivity().getBaseContext(), databaseReference, chatFriendsIdList);
            listMessageFromFriendRecyclerView.setLayoutManager(new LinearLayoutManager(getActivity().getBaseContext()));
            listMessageFromFriendRecyclerView.setHasFixedSize(true);
            listMessageFromFriendRecyclerView.setAdapter(listMessageFromFriendRecyclerViewAdapter);
            listMessageFromFriendRecyclerViewAdapter.notifyDataSetChanged();

        }

        return view;
    }
}