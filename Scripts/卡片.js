

	function cardhint(element) {
					return element.clone().css("width", element.width()).removeClass("k-state-selected");
}

				function cardplaceholder(element) {
					return element.clone().css("opacity", 0.1);
}
function cardLeftscrollButtonClick() {
	var scrollContainer = $(".k-card-deck").eq(0);
	var lastCard = scrollContainer.find(".k-card").last();
	var cardWidth = lastCard.outerWidth(true);
	scrollContainer.scrollLeft(scrollContainer.scrollLeft() - cardWidth);
	
	
}
function cardRightscrollButtonClick() {

	var scrollContainer = $(".k-card-deck").eq(0);
	var lastCard = scrollContainer.find(".k-card").last();
	var cardWidth = lastCard.outerWidth(true);
	scrollContainer.scrollLeft(scrollContainer.scrollLeft() + cardWidth);
};
function scrollButtonClick(e) {
	var button = $(e.currentTarget);
	var scrollToLeft = button.find(".k-i-arrow-chevron-left").length !== 0;
	var scrollContainer = $(".k-card-deck").eq(0);
	var lastCard = scrollContainer.find(".k-card").last();
	var cardWidth = lastCard.outerWidth(true);


	if (scrollToLeft) {
		scrollContainer.scrollLeft(scrollContainer.scrollLeft() - cardWidth);
	} else {
		scrollContainer.scrollLeft(scrollContainer.scrollLeft() + cardWidth);
	}
};
				
