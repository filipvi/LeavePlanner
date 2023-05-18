$('.js-getHelpModal').on('click',
    function () {
        handleGetHelpModal();
    });

function handleGetHelpModal(){
    $('.js-helpModal').removeClass('d-none');
    $('#helpModal').modal('show');
}

function definePdvMultiplier(pdv) {
    
    let pdvEnums = {
        pdv0: 5,
        pdv5: 4,
        pdv10: 3,
        pdv13: 2,
        pdv25: 1
    };
    
    let pdvMultiplier = 1;
    
    switch (pdv) {
        case pdvEnums.pdv13:
            pdvMultiplier = 1.13;
            break;
        
        case pdvEnums.pdv10:
            pdvMultiplier = 1.10;
            break;
        
        case pdvEnums.pdv5:
            pdvMultiplier = 1.05;
            break;
        
        case pdvEnums.pdv0:
            pdvMultiplier = 1;
            break;
        case pdvEnums.pdv25:
            pdvMultiplier = 1.25;
            break;
        
        default:
            pdvMultiplier = 0;
            break;
    }
    
    return pdvMultiplier;
}

function isFloat(val) {
    let floatRegex = /^(?!0*[.,]0*$|[.,]0*$|0*$)\d+[,.]?\d{0,6}$/;
    if (!floatRegex.test(val))
        return false;
    val = parseFloat(val);
    return !isNaN(val);
}

function isOibValid(input) {
    let oib = input.toString();
    
    if (oib.match(/\d{11}/) === null) {
        return false;
    }
    
    let calculated = 10;
    
    for (let digit of oib.substring(0, 10)) {
        calculated += parseInt(digit);
        
        calculated %= 10;
        
        if (calculated === 0) {
            calculated = 10;
        }
        
        calculated *= 2;
        
        calculated %= 11;
    }
    
    let check = 11 - calculated;
    
    if (check === 10) {
        check = 0;
    }
    
    return check === parseInt(oib[10]);
}

function preciseRound(num, decimals) {
    let t = Math.pow(10, decimals);
    return (Math.round((num * t) + (decimals > 0 ? 1 : 0) * (Math.sign(num) * (10 / Math.pow(100, decimals)))) / t)
        .toFixed(decimals);
}

jQuery.fn.preventDoubleSubmission = function () {
    $(this).on('submit',
        function (e) {
            let $form = $(this);
            if ($form.data('submitted') === true) {
                // Previously submitted - don't submit again
                e.preventDefault();
            } else {
                // Mark it so that the next submit can be ignored
                if ($form.valid()) {
                    $form.data('submitted', true);
                }
            }
        });
    // Keep chainability
    return this;
};

markInputsRequired = function () {
    let selector =
        'input:not(:disabled)[type=tel], input:not(:disabled)[type=mail], input:not(:disabled)[type=email], input:not(:disabled)[type=text], input:not(:disabled)[type=file], input:not(:disabled)[type=select], textarea:not(:disabled), select:not(:disabled)';
    $(selector)
        .each(function () {
            let req = $(this).attr('data-val-required');
            if (undefined !== req) {
                let label = $('label[for="' + $(this).attr('id') + '"]');
                let text = label.text();
                if (text.length > 0 && !text.includes("*")) {
                    label.append('<span style="color:#b22222"> *</span>');
                }
            }
        });
}

initTooltip = function(){
    $('[data-toggle="tooltip"]').tooltip();
}

function countChars(obj, maxLength, containerId) {
    document.getElementById(containerId).innerHTML = 'Uneseno ' + obj.value.length + ' od dopuštenih ' + maxLength + ' znakova';
}

function scrollToBottom(duration) {
    setTimeout(
        function () {
            $("html, body").animate({scrollTop: $(document).height()}, 1000);
        }, duration != null ? duration : 1500);
}

function scrollToTop(duration) {
    setTimeout(
        function () {
            $("html, body").animate({scrollTop: 0}, 1000);
        }, duration != null ? duration : 1500);
}

let initializeTabs = function (tabId) {
    
    // Svim ddl uklanjamo active klase
    $('.js-ddl').each(function () {
        $(this).removeClass('active');
    });
    
    //Svakom elementu sa klasom tab-pane uklanjamo aktivne klase
    $('.js-tab-pane').each(function () {
        $(this).removeClass('active').removeClass('show');
    });
    
    // Svakom elementu sa klasom nav-link uklanjamo aktivne klase
    $(".js-nav-link").each(function () {
        $(this).removeClass('active').removeClass('show');
        $(this).prop('aria-selected', false);
    });
    
    // incijalizacija početnih vrijednosti
    let selectorTab;
    let selectorLink;
    
    // Ovisno da li je poslan tab Id postavljamo varijable
    if (tabId != null && tabId !== '') {
        selectorTab = "#" + tabId;
        selectorLink = selectorTab + "Link";
    } else {
        selectorTab = "#tabOsnovniInfo";
        selectorLink = selectorTab + "Link";
    }
    
    // Elementu sa traženom klasom stavljamo active klasu
    $(selectorLink).addClass('active').prop('aria-selected', true);
    $(selectorTab).addClass('active').addClass('show');
    
    if ($('.js-nav-link.active').parents('.dropdown').length) {
        $('.js-nav-link.active').parent().parent().find('.js-ddl').addClass('active');
    }
}
