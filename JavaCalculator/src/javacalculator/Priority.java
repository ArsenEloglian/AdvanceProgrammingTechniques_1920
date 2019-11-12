/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package javacalculator;

/**
 *
 * @author studia
 */
public class Priority {

    public static int status(String operator) {
        if (operator.equals("+") || operator.equals("-")) {
            return 1;
        } // dla * i / zwracamy 2
        else if (operator.equals("*") || operator.equals("/")) {
            return 2;
        } // dla pozostałych 0
        else {
            return 0;
        }
    }
}
