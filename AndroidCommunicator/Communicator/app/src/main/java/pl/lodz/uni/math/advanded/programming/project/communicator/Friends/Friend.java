package pl.lodz.uni.math.advanded.programming.project.communicator.Friends;

public class Friend {

    private String friendName;
    private String friendUrl;

    public Friend(String friendName, String friendUrl) {
        this.friendName = friendName;
        this.friendUrl = friendUrl;
    }

    public String getFriendName() {
        return friendName;
    }

    public void setFriendName(String friendName) {
        this.friendName = friendName;
    }

    public String getFriendUrl() {
        return friendUrl;
    }

    public void setFriendUrl(String friendUrl) {
        this.friendUrl = friendUrl;
    }
}
