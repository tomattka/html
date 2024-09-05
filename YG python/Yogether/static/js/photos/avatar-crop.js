$(document).ready(function(){
  loadCropper();
  $("#bSave").click(savePicture);
  $("#bCancel").click(cancelClick);
});

// callin cropper on resize end
var doit;
window.onresize = function(){
  clearTimeout(doit);
  doit = setTimeout(function(){
    $('#full-photo').cropper('destroy');
    loadCropper();
  }, 100);
};

function savePicture(){
  $('#fCrop').submit();
}

function cancelClick(){
  $("#iDelete").val("true");
  $('#fCrop').submit();
}

function loadCropper(){
  var newImg = new Image();
  newImg.onload = function(){    
    var $image = $('#full-photo');
    try{
    var origWidth = newImg.width,
        currWidth = $image.width(),
        minCropWidth = Math.round(270 * currWidth / origWidth);
    }
    catch{
      minCropWidth = 270;
    }
    $image.cropper({
      aspectRatio: 1 / 1,
      viewMode: 2,
      autoCropArea: 1,
      minCropBoxWidth: minCropWidth,
      rotatable: false,
      scalable: false,
      zoomable: false,
      preview: '.preview',
      crop: function(event) {
        $('#iX').val(Math.round(event.detail.x));
        $('#iY').val(Math.round(event.detail.y));
        $('#iW').val(Math.round(event.detail.width));
      }
    });
  }
  newImg.src = $('#full-photo').attr("src");  
}
