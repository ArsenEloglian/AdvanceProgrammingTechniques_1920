/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package javacalculator;

import java.util.Stack;

/**
 *
 * @author studia
 */
public class ONPNotation implements NotationInterface {
    private Stack<String> parts;
    private String notation;
    
    public ONPNotation()
    {
        parts.clear();
        this.parts = new Stack<String>();
    }

    public void setNotation(String notation) {
        this.notation = notation;
    }
    
    public void add(String text) 
    {
        this.parts.push(text);
    }
    public String remove()
    {
        return this.parts.pop();
    }
    public String getNotation()
    {
        return this.notation;
    }
    
    public boolean empty() {
        return this.parts.empty();
    }
    
    public String next() {
        return this.parts.peek();
    }
    
    public Float calculate(){
        return 0.0f;
    }
}
