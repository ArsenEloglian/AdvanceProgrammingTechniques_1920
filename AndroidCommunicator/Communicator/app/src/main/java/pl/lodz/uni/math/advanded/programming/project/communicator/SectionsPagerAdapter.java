package pl.lodz.uni.math.advanded.programming.project.communicator;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentPagerAdapter;

class SectionsPagerAdapter extends FragmentPagerAdapter {
    public SectionsPagerAdapter(FragmentManager fm) {
        super(fm);
    }

    @Override
    public Fragment getItem(int position) {
        switch (position) {
            case 0:
                ListMessageFromFriendsFragment listMessageFromFriendsFragment = new ListMessageFromFriendsFragment();
                return listMessageFromFriendsFragment;

            case 1:
                FriendsFragment friendsFragment = new FriendsFragment();
                return friendsFragment;


            default:
                return null;

        }
    }

    @Override
    public int getCount() {
        return 2;
    }

    public CharSequence getPageTitle(int position) {
        switch (position) {
            case 0:
                return "Wiadomosci";

            case 1:
                return "Znajomi";

            default:
                return null;
        }
    }
}