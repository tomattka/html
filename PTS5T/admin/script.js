// JavaScript Document
function showAdditional(){
	if (document.getElementById('additional').style.display === 'none')
		{
			document.getElementById('additional').style.display = 'block';
			document.getElementById('show').innerHTML = "Скрыть"
		}
	else
		{
			document.getElementById('additional').style.display = 'none';
			document.getElementById('show').innerHTML = "Дополнительно"
		}
}

function copyText(elementId) {
    document.getElementById(elementId).select();
    document.execCommand("copy");
    alert("Ссылка скопирована в буфер обмена");
}