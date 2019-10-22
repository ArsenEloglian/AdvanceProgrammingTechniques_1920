package javacalculator;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.*;
import javax.swing.UIManager.LookAndFeelInfo;


/**
 *
 * @author fatne, morrison
 */

public class JavaCalculator implements ActionListener {
   
   private JTextField field;
   private String operation;
   
   public final String[][] BUTTON_TEXTS = {
      {"C", "CE", "%", "√"},
      {"7", "8", "9", "+"},
      {"4", "5", "6", "-"},
      {"1", "2", "3", "*"},
      {"0", ".", "/", "="}
   };
   public final Font BTN_FONT = new Font(Font.SERIF, Font.PLAIN, 24);

   private void createAndShowGui() {
      field = new JTextField(10);
      field.setPreferredSize(new Dimension(0, 80));
      field.setFont(BTN_FONT.deriveFont(Font.PLAIN));
      JPanel btnPanel = new JPanel(new GridLayout(BUTTON_TEXTS.length,
            BUTTON_TEXTS[0].length));
      btnPanel.setPreferredSize(new Dimension(244, 300));

      for (int i = 0; i < BUTTON_TEXTS.length; i++) {
         for (int j = 0; j < BUTTON_TEXTS[i].length; j++) {
            JButton btn = new JButton(BUTTON_TEXTS[i][j]);
            btn.addActionListener(this);
            btn.setForeground(new Color(100, 100, 100));
            btn.setFont(BTN_FONT);
            btn.setFocusPainted(false);
            btnPanel.add(btn);
         }
      }

      JPanel mainPanel = new JPanel(new BorderLayout());
      mainPanel.add(field, BorderLayout.PAGE_START);
      mainPanel.add(btnPanel, BorderLayout.CENTER);

      JFrame frame = new JFrame("For Git labs");
      frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
      frame.getContentPane().add(mainPanel);
      frame.pack();
      frame.setResizable(false);
      frame.setLocationRelativeTo(null);
      frame.setVisible(true);
   }

   public static void main(String[] args) {    
        try {
            for (LookAndFeelInfo info : UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (Exception e) {
            
        }
       
      SwingUtilities.invokeLater(new Runnable() {
         public void run() {
           new JavaCalculator().createAndShowGui();
         }
      });
   }

    @Override
    public void actionPerformed(ActionEvent e) {              
        operation = e.getActionCommand();
        
        if(!this.isInteger(operation)){
            switch(operation){
                case "C" :       
                    break;
                case "CE" : 
                    break;
                case "%" :       
                    break;      
                case "√" : 
                    break;
                case "+" : 
                    break;
                case "-" : 
                    break;
                case "*" : 
                     break;
                case "=" : 
                    break;
                case "/" : 
                    break;
                case "." : 
                    break;
            }
        }else {    
        }
    }
    private boolean isInteger(String s) {
        try { 
            Integer.parseInt(s); 
        } catch(NumberFormatException e) { 
            return false; 
        } catch(NullPointerException e) {
            return false;
        }
        return true;
    }
}
