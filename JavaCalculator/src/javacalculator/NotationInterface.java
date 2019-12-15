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
public interface NotationInterface {
    void add(String text);
    String remove();
    String getNotation();
    boolean empty();
    String next();
    void setNotation(String notation);
    Float calculate();
}
