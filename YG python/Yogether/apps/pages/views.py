from django.shortcuts import render
from django.views.generic.base import View
from userProfile.forms import YGLoginForm
from .models import YgPage


# Create your views here.
class Page(View):
    def get(self, request, alias):
        page_title = "title"
        page_text = "text"
        page = YgPage.objects.get(alias=alias)
        page_title = page.title
        page_text = page.text
        return render(request, "ygPages/page.html", {"form": YGLoginForm, "page_title": page_title, "page_text": page_text})
