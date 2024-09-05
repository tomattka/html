from django.shortcuts import render
from django.views.generic.base import View
from .models import YgRecommendRemove


class RecommendRemove(View):
    def get(self, request):
        try:
            self_id = request.GET["self_id"]
            remove_id = request.GET["remove_id"]
            row, created = YgRecommendRemove.objects.get_or_create(user_self_id=self_id, user_remove_id = remove_id)
            result = 'success'
        except:
            result = 'error'
        return render(request, 'ygProfile/result-only.html', {'result': result})


class RecommendCancelRemove(View):
    def get(self, request):
        try:
            self_id = request.GET["self_id"]
            remove_id = request.GET["remove_id"]
            row = YgRecommendRemove.objects.get(user_self_id=self_id, user_remove_id = remove_id)
            row.delete()
            result = 'success'
        except:
            result = 'none'
        return render(request, 'ygProfile/result-only.html', {'result': result})


class RecommendReset(View):
    def get(self, request):
        user_id = request.GET["user_id"]
        recommends = YgRecommendRemove.objects.filter(user_self_id=user_id)
        recommends.delete()
        result = 'success'
        return render(request, 'ygProfile/result-only.html', {'result': result})
