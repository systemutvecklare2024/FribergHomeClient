window.getWindowWidth = () => {
	return window.innerWidth;
};

window.listenForResize = (dotnetObj) => {

    window.removeEventListener('resize', window._resizeHandler); //remove eventlistener

    window._resizeHandler = () => { 
        dotnetObj.invokeMethodAsync('UpdateWidth', window.innerWidth);
    };

    window.addEventListener('resize', window._resizeHandler); //Add eventlistener 
};