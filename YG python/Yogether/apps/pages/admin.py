from django.contrib import admin
from .models import YgPage

from django import forms
from ckeditor_uploader.widgets import CKEditorUploadingWidget


class PageAdminForm(forms.ModelForm):
    text = forms.CharField(widget=CKEditorUploadingWidget())

    class Meta:
        model = YgPage
        fields = '__all__'


class PageAdmin(admin.ModelAdmin):
    form = PageAdminForm


# Register your models here.
admin.site.register(YgPage, PageAdmin)
