/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package javacalculator;

import java.util.StringTokenizer;

/**
 *
 * @author studia
 */
public class Expression {

    String text;

    public Expression(String text) {
        this.text = text;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public NotationInterface parseToNotation(NotationInterface notation) {

        StringTokenizer st = new StringTokenizer(this.text, "+-*/()", true);
        String expressionPrefix="";
        while (st.hasMoreTokens()) {

            // pobieramy element
            String s = st.nextToken();

            // jeżeli element jest operatorem
            if (s.equals("+") || s.equals("*") || s.equals("-") || s.equals("/")) {
                // sprawdzemy priorytety
                while (!notation.empty() && Priority.status(notation.next()) >= Priority.status(s)) {
                    expressionPrefix += notation.remove() + " ";
                            System.out.println(expressionPrefix);

                }
                // odkładamy opexpressionPrefixrator na stos
                notation.add(s);
            } // jeżeli element jest nawiasem otwierającym
            else if (s.equals("(")) {
                notation.add(s);
            } // jeżeli element jest nawiasem zamykającym
            else if (s.equals(")")) {
                // ściągamy operatory ze stosu, aż do nawiasu otwierajęcego
                while (!notation.next().equals("(")) {
                    expressionPrefix += notation.remove() + " ";
                }
                // ściągamy nawias otwierający
                notation.remove();
            } // jeżeli element nie jest operatorem ani nawiasem dodajemy go do wyrażenia postfiksowego
            else {
                 expressionPrefix += s + " ";
            }
        }
        // ściągamy ze stosu pozostałe operatory i dodajemy je do wyrażenia postfiksowego
        while (!notation.empty()) {
            expressionPrefix += notation.remove() + " ";
        }

        notation.setNotation(expressionPrefix);
        
        return notation;
    }
}
