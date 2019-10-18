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
public:
    Calculator(QWidget *parent = 0);

private slots:
    void digitClicked();
    void sumClicked();
//    void subClicked();
//    void mulClicked();
//    void divClicked();
    void pointClicked();
    void equalClicked();
};

#endif // CALCULATOR_H
