#ifndef CALCULATOR_H
#define CALCULATOR_H

#include <QWidget>
#include <QLineEdit>
class Button;


class Calculator : public QWidget
{
    Q_OBJECT
private:
    int NumDigitButtons = 10;
    QLineEdit *display;
    Button *createButton(const QString &text, const char *slot);
    bool calculate(double rightOperand, const QString &pendingOperator);
    Button *digitButtons[10];
    bool iswaitingOperand;
    double Result;
    bool isAdd;
    bool isSubstract;
    bool isMultiply;
    bool isDivide;
    bool isPow;
    bool isSqrt;
    double firstValue,secondValue;

public:
    Calculator(QWidget *parent = 0);

private slots:
    void powClicked();
    void sqrtClicked();
    void digitClicked();
    void plusClicked();
    void minusClicked();
    void multClicked();
    void divisionClicked();
    void clearClicked();
    void pointClicked();
    void equalClicked();
    void backspaceClicked();
    void signClicked();
    void percentClicked();
    void clearMemoryClicked();
    void addToMemoryClicked();
};

#endif // CALCULATOR_H
