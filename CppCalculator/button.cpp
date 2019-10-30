#include "button.h"
#include <QtWidgets>

Button::Button(const QString &text, QWidget *parent): QToolButton(parent)
{
    setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Preferred);
    setText(text);
}
