/* 
Для каждой карусели заводим свой список значений, в котором хранятся данные от конкретного слайдера

itemsCount - количество элементов
itemWidth - ширина элемента
marginRight - отступ справа элемента
itemsDisplayed - количество отображаемых элементов
moveWidth - ширина сдвига
currentPos - текущая позиция (номер элемента)
sliderWidth - ширина ползунка
moveLeftPossible, moveLeftPossible - возможно ли сдвигать влево и вправо

(если элементов ниже нет, то назначаем 'none')
leftButtonId - id левой кнопки
rightButtonId - id правой кнопки
baseId - id основания слайдера
sliderId - id ползунка
itemsId - id ленты элементов
frameId - id рамки (видимой части)
 */

 var panelsCarousel = {};
 var popularCarousel = {};

/* DOCUMENT READY */
$(document).ready(function(){
    setConstants(); // Назначаем id элементов
    
    /* Рассчёт значений */
    calcValues(panelsCarousel); // Рисунок МДФ панелей
    setSliderSize(panelsCarousel);
    calcValues(popularCarousel); // Популярные модели

    setEvents(); // Назначаем события на элементы

});

/* WINDOW RESIZE */
$(window).resize(function(){    
    calcValues(panelsCarousel); // Перерасчёт значений
    setSliderSize(panelsCarousel);
    calcValues(popularCarousel);
    
    resetPosition(panelsCarousel); // Сброс слайдеров при ресайзе
    resetPosition(popularCarousel);
});

function setConstants(){ // Здесь назначаются Id элементов
    panelsCarousel.leftButtonId = '#bPnlLeft';
    panelsCarousel.rightButtonId = '#bPnlRight';
    panelsCarousel.baseId = '#pnlSliderBase';
    panelsCarousel.sliderId = '#pnlSlider';
    panelsCarousel.itemsId = '#pnlItems';
    panelsCarousel.frameId = '#pnlFrame';

    
    popularCarousel.leftButtonId = popularCarousel.rightButtonId = popularCarousel.baseId = popularCarousel.sliderId = 'none';
    popularCarousel.itemsId = '#popularItems';
    popularCarousel.frameId = '#popularFrame';
}

function setEvents(){ // Назначаем события на элементы
    $(panelsCarousel.leftButtonId).click(function(){ moveItemsLeft(panelsCarousel); return false; });
    $(panelsCarousel.rightButtonId).click(function(){ moveItemsRight(panelsCarousel); return false; });
}

function calcValues(groupParams){
    groupParams.currentPos = 0;
    items = $(groupParams.itemsId + " > .item");
    groupParams.itemsCount = items.length;
    groupParams.itemWidth = items.first().outerWidth();
    groupParams.marginRight = parseInt(items.first().css("margin-right"));
    frameWidth = $(groupParams.frameId).width();

    groupParams.itemsDisplayed = parseInt((frameWidth + groupParams.marginRight) / (groupParams.itemWidth + groupParams.marginRight)); // общая длина минус отступ, делить на ширину + отступ
    if (groupParams.itemsDisplayed == 0)
        groupParams.itemsDisplayed = 1;
    groupParams.moveWidth = (groupParams.itemWidth + groupParams.marginRight) * groupParams.itemsDisplayed;
    if (groupParams.itemsCount <= groupParams.itemsDisplayed)
        setBtnState('Right', false, groupParams.rightButtonId, groupParams);

    // Возможность сдвига
    groupParams.moveLeftPossible = false;
    groupParams.moveRightPossible = true;
    
    // Выставление изначальных значений кнопок   
        if (groupParams.sliderId != 'none'){
        setBtnState('Left', 0, groupParams.leftButtonId, groupParams); 
        if (groupParams.itemsCount > groupParams.itemsDisplayed)
            setBtnState('Right', 1, groupParams.rightButtonId, groupParams); 
    } 
}

function resetCarouselPosition(groupParams){
    groupParams.currentPos = 0;
    $(groupParams.itemsId).css('margin-left', '0');
    $(groupParams.sliderId).css('margin-left', '0');
    setBtnState('Left', 0, groupParams.leftButtonId); 
    if (groupParams.itemsCount > groupParams.itemsDisplayed)
        setFbBtnState('Right', 1, groupParams.rightButtonId);
}

function moveItemsLeft(groupParams){
    if (groupParams.moveLeftPossible)
    {
        groupParams.currentPos -= groupParams.itemsDisplayed;
        $(groupParams.itemsId).animate({
            marginLeft: '+=' + groupParams.moveWidth + 'px'
        }, 500);

        /* Если нельзя больше двигать влево, отключаем кнопку */
        if (groupParams.currentPos < groupParams.itemsDisplayed)
            setBtnState('Left', false, groupParams.leftButtonId, groupParams);   

        /* Делаем правую кнопку активной */
        if (!groupParams.moveRightPossible)
            setBtnState('Right', true, groupParams.rightButtonId, groupParams);

        moveSlider(-1, groupParams);
        
    }    
}

function moveItemsRight(groupParams){
    if (groupParams.moveRightPossible)
    {
        groupParams.currentPos += groupParams.itemsDisplayed;
        $(groupParams.itemsId).animate({
            marginLeft: '-=' + groupParams.moveWidth + 'px'
        }, 500);

        /* Если нельзя больше двигать вправо, отключаем кнопку */
        if (groupParams.currentPos + groupParams.itemsDisplayed >= groupParams.itemsCount)
            setBtnState('Right', false, groupParams.rightButtonId, groupParams);   


        /* Делаем левую кнопку активной */
        if (!groupParams.moveLeftPossible)
        {
            setBtnState('Left', true, groupParams.leftButtonId, groupParams); 
        }

        moveSlider(1, groupParams);

    }
}

function setBtnState(btnSide, isActive, btnId, groupParams){ // Пример: setFbBtnState('Left', true, '#bPnlLeft')
    if (btnId != 'none')
    {
        bgPath = 'img/arrow_' + btnSide.toLowerCase();
        if (!isActive)
            bgPath += '_inactive';    
        bgPath += '.png';
        $(btnId).css("background-image", "url('" + bgPath + "')");  
    }
    if (btnSide == 'Left')
        groupParams.moveLeftPossible = isActive;
    else
    groupParams.moveRightPossible = isActive;

}

/* Установка размера слайдера */
function setSliderSize(groupParams){
    fullWidth = $(groupParams.baseId).width() - $(groupParams.leftButtonId).outerWidth() - $(groupParams.rightButtonId).outerWidth();
    if (groupParams.itemsDisplayed > 1)
        pagesCount = parseInt((groupParams.itemsCount + 1) / groupParams.itemsDisplayed);
    else
        pagesCount = groupParams.itemsCount;
    groupParams.sliderWidth = parseInt(fullWidth / pagesCount);
    $(groupParams.sliderId).width(groupParams.sliderWidth);
}

function moveSlider(sliderPos, groupParams){ // 1 = вправо and -1 = влево
    $(groupParams.sliderId).animate({
        marginLeft: '+=' + sliderPos * groupParams.sliderWidth + 'px'
    }, 500);
}

function resetPosition(groupParams){
    groupParams.currentPos = 0;    
    $(groupParams.itemsId).css('margin-left', '0');
    if (groupParams.sliderId != 'none')
        $(groupParams.sliderId).css('margin-left', '0');   
}