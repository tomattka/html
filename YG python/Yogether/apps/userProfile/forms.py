from allauth.account.forms import LoginForm, SignupForm
from django import forms


class YGSignupForm(SignupForm):
    first_name = forms.CharField(label='Имя')  # field is required by default
    first_name.widget.attrs.update({'placeholder': 'Имя'})

    last_name = forms.CharField(label='Фамилия')
    last_name.widget.attrs.update({'placeholder': 'Фамилия'})

    def save(self, request):
        # Ensure you call the parent class's save.
        # .save() returns a User object.
        user = super(YGSignupForm, self).save(request)

        # Add your own processing here.
        user.first_name = self.cleaned_data['first_name']
        user.last_name = self.cleaned_data['last_name']
        user.save()

        # You must return the original result.
        return user


class YGLoginForm(LoginForm):

    def __init__(self, *args, **kwargs):
        super(YGLoginForm, self).__init__(*args, **kwargs)
        self.fields['login'].widget.attrs.update({
            'class': 'login-form__input login-form__input_login',
            'placeholder': 'Логин'
        })
        self.fields['password'].widget.attrs.update({
            'class': 'login-form__input login-form__input_password'
        })
        self.fields['remember'].widget.attrs.update({
            'checked': 'true',
            'style': 'display: none;'
        })

    def login(self, *args, **kwargs):

        # Add your own processing here.

        # You must return the original result.
        return super(YGLoginForm, self).login(*args, **kwargs)