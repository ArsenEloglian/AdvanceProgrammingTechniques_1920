package pl.lodz.uni.math.advanded.programming.project.communicator.Friends;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Filter;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import pl.lodz.uni.math.advanded.programming.project.communicator.R;

public class FriendAdapter extends ArrayAdapter<Friend> {

    private Context mContext;
    private ArrayList<Friend> friends;
    private ArrayList<Friend> filterFriendsList;

    public FriendAdapter(@NonNull Context mContext, ArrayList<Friend> friendArrayList) {
        super(mContext, 0, friendArrayList);
        this.mContext = mContext;
        this.friends = friendArrayList;
        this.filterFriendsList = friendArrayList;
    }

    @Override
    public int getCount() {
        return friends.size();
    }

    @NonNull
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View listItem = convertView;
        if (listItem == null) {
            //LayoutInflatersłuży do tworzenia nowego View(lub Layout) obiektu z jednego z układów xml
            listItem = LayoutInflater.from(mContext).inflate(R.layout.list_item, parent, false);
        }

        TextView name = listItem.findViewById(R.id.friendSearchItem);
        name.setText(friends.get(position).getFriendName());

        return listItem;
    }


    @NonNull
    @Override
    public Filter getFilter() {
        return new Filter() {
            @Override
            protected FilterResults performFiltering(CharSequence charSequence) {

                FilterResults results = new FilterResults();

                if (charSequence != null && charSequence.length() > 0) {
                    charSequence = charSequence.toString().toUpperCase();

                    ArrayList<Friend> filtersFriends = new ArrayList<Friend>();

                    for (int i = 0; i < filterFriendsList.size(); i++) {
                        if (filterFriendsList.get(i).getFriendName().toUpperCase().contains(charSequence)) {
                            Friend friend = new Friend(filterFriendsList.get(i).getFriendName(), filterFriendsList.get(i).getFriendUrl());

                            filtersFriends.add(friend);
                        }
                    }

                    results.count = filtersFriends.size();
                    results.values = filtersFriends;
                } else {
                    results.count = filterFriendsList.size();
                    results.values = filterFriendsList;
                }
                return results;
            }

            @Override
            protected void publishResults(CharSequence charSequence, FilterResults filterResults) {
                friends = (ArrayList<Friend>) filterResults.values;
                if (filterResults.count > 0) {
                    notifyDataSetChanged();
                } else {
                    notifyDataSetInvalidated();
                }
            }
        };
    }
}
