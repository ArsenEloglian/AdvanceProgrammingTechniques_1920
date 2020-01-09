package pl.lodz.newqqq;

import androidx.appcompat.app.AppCompatActivity;

import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.util.Properties;

import javax.mail.AuthenticationFailedException;
import javax.mail.Folder;
import javax.mail.Message;
import javax.mail.MessagingException;
import javax.mail.NoSuchProviderException;
import javax.mail.Session;
import javax.mail.Store;

public class MainActivity extends AppCompatActivity {
    private EditText user;
    private EditText pass;
    private EditText subject;
    private EditText body;
    private EditText recipient;
    private EditText response;
    Button button;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        button = (Button) findViewById(R.id.buttonWyslij);
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                sendMessage();
            }
        });

        user = (EditText) findViewById(R.id.username);
        pass = (EditText) findViewById(R.id.password);
        subject = (EditText) findViewById(R.id.subject);
        body = (EditText) findViewById(R.id.body);
        recipient = (EditText) findViewById(R.id.recipient);
        response = (EditText) findViewById(R.id.response);

        pass.setText("****");
        check("poczta.o2.pl", "imap", "prybak2019@o2.pl", "****");
    }
    private void sendMessage() {
        String[] recipients = { recipient.getText().toString() };
        SendEmailAsyncTask email = new SendEmailAsyncTask();
        email.activity = this;
        email.m = new Mail(user.getText().toString(), pass.getText()
                .toString());
        email.m.set_from(user.getText().toString());
        email.m.setBody(body.getText().toString());
        email.m.set_to(recipients);
        email.m.set_subject(subject.getText().toString());
        email.execute();
    }
    public void check(String host, String storeType, String user,
                             String password)
    {
        try {

            //create properties field
            Properties properties = new Properties();

            properties.put("mail.pop3.host", host);
            properties.put("mail.pop3.port", "993");
            properties.put("mail.pop3.starttls.enable", "true");
            Session emailSession = Session.getDefaultInstance(properties);

            //create the POP3 store object and connect with the pop server
            Store store = emailSession.getStore("imaps");

            store.connect(host, user, password);
/*
            //create the folder object and open it
            Folder emailFolder = store.getFolder("INBOX");
            emailFolder.open(Folder.READ_ONLY);

            // retrieve the messages from the folder in an array and print it
            Message[] messages = emailFolder.getMessages();
            response.setText("messages.length---" + messages.length);

            for (int i = 0, n = messages.length; i < n; i++) {
                Message message = messages[i];
                System.out.println("---------------------------------");
                System.out.println("Email Number " + (i + 1));
                System.out.println("Subject: " + message.getSubject());
                System.out.println("From: " + message.getFrom()[0]);
                System.out.println("Text: " + message.getContent().toString());

            }

            //close the store and folder objects
            emailFolder.close(false);
            store.close();
*/
        } catch (NoSuchProviderException e) {
            response.setText("NoSuchProviderException");
        } catch (MessagingException e) {
            response.setText("MessagingException");
            e.printStackTrace();
        } catch (Exception e) {
            response.setText("Exception"+e.toString()+e.getMessage());
        }
    }
}
class SendEmailAsyncTask extends AsyncTask<Void, Void, Boolean> {
    Mail m;
    MainActivity activity;

    public SendEmailAsyncTask() {}
void alertUser(final String withWhat){
    final Button button = activity.button;
    button.post(new Runnable() {
        public void run() {
            button.setText(withWhat);
        }
    });
}
    @Override
    protected Boolean doInBackground(Void... params) {
        try {
            if (m.send()) {
                alertUser("Sent.");
            } else {
                alertUser("Failed to send.");
            }
            return true;
        } catch (AuthenticationFailedException e) {
            alertUser("Authentication failed.");
            return false;
        } catch (MessagingException e) {
            alertUser("Email failed to send.");
            return false;
        } catch (Exception e) {
            alertUser("Unexpected error occured.");
            return false;
        }
    }
}
