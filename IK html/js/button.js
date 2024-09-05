protectAnimation = false; // включение анимации кнопки

$(document).ready(function(){
    $('.red-button').mouseenter(animateRedButton);
});


function animateRedButton(){
    if (!protectAnimation)
    {
        protectAnimation = true;
        rightBlock = $(this).find('.animation-right')
        bottomBlock = $(this).find('.animation-bottom')
        // запоминаются размеры перекрывающих панелей
        rHeight = rightBlock.height();
        bWidth = bottomBlock.width();
        // отображаем панели
        rightBlock.css('visibility', 'visible');
        bottomBlock.css('visibility', 'visible');
        // ширина нижней анимируется до нуля
        bottomBlock.animate({
            width: '0',
        }, 300, function(){
            // высота верхней анимируется до нуля
            rightBlock.animate({height: 0,}, function(){            
                rightBlock.css({"visibility": "hidden", "height": rHeight + "px"});
                bottomBlock.css({"visibility": "hidden", "width": bWidth + "px"});
                protectAnimation = false;
            });
        }
        );
    }
}