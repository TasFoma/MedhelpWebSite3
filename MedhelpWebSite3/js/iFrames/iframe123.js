document.addEventListener('contextmenu', function (e) {
    e.preventDefault();
});
function ShowFrame() {
    var href = "https://medhelponline.ru/ServicesPrice.aspx?BusinessID=b1TFYQutXH4plOi";
    var isMobile = false; //initiate as false
    // device detection
    if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
        || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) isMobile = true;
    if (isMobile) {        
        window.open(href, '_blank').focus();
        return false;
    }
    $('<div>', {
        id: 'medhelpDivBackground',
        css: {
            width: '100%',
            height: '100%',
            position: 'fixed',
            top: '0',
            left: '0',
            zIndex: '9999',
            background: 'rgba(0,0,0,0.8)'
        },
    }).appendTo('#iframeContainer');
    $('<div>', {
        id: 'medhelpDivContainer',
        css: {
            minWidth: '550px',
            maxWidth: '900px',
            width: '80%',
            height: '90%',
            position: 'fixed',
            top: '50%',
            left: '50%',
            zIndex: '9999',
            borderRadius: '20px',
            transform: 'translate(-50%, -50%)',
            background: 'rgba(128, 128, 128, 0.8)'
        },
    }).appendTo('#medhelpDivBackground');
    $('<div>', {
        id: 'divLoader',
        css: {
            background: 'transparent',
            textAlign: 'center',
            position: 'fixed',
            width: '240px',
            height: '240px',
            left: 'Calc(50% - 120px)',
            top: 'Calc(50% - 120px)',
            zIndex: '9999'
        }
    }).appendTo('#medhelpDivContainer');
    $('<div>', {
        id: 'cubeGrid',
        class: 'sk-cube-grid'
    }).appendTo('#divLoader');
    $('<div>', {
        class: 'sk-cube sk-cube1'
    }).appendTo('#cubeGrid');
    $('<div>', {
        class: 'sk-cube sk-cube2'
    }).appendTo('#cubeGrid');
    $('<div>', {
        class: 'sk-cube sk-cube3'
    }).appendTo('#cubeGrid');
    $('<div>', {
        class: 'sk-cube sk-cube4'
    }).appendTo('#cubeGrid');
    $('<div>', {
        class: 'sk-cube sk-cube5'
    }).appendTo('#cubeGrid');
    $('<div>', {
        class: 'sk-cube sk-cube6'
    }).appendTo('#cubeGrid');
    $('<div>', {
        class: 'sk-cube sk-cube7'
    }).appendTo('#cubeGrid');
    $('<div>', {
        class: 'sk-cube sk-cube8'
    }).appendTo('#cubeGrid');
    $('<div>', {
        class: 'sk-cube sk-cube9'
    }).appendTo('#cubeGrid');
    $('<div>', {
        id: 'medhelpDivClose',
        text: 'Закрыть',
        css: {
            width: '100px',
            height: '31px',
            fontSize: '15px',
            fontWeight: '400',
            fontFamily: "'Open Sans', sans-serif",
            position: 'absolute',
            top: '-13px',
            right: '-13px',
            zIndex: '9999',
            borderRadius: '10px',
            background: '#C60000',
            border: 'solid 2px #8a0000',
            paddingTop: '4px',
            textAlign: 'center',
            color: '#fff',
            cursor: 'default'
        },
        on: {
            click: function (event) {
                $('#medhelpDivBackground').remove();
            },
            mouseover: function (event) {
                $('#medhelpDivClose').css('cursor', 'pointer');
            }
        }
    }).appendTo('#medhelpDivContainer');
    $('<iframe>', {
        id: 'medhelponline',
        frameborder: 0,
        on: {
            load: function (e) {
                $('#divLoader').remove();
            }
        },
        src: href,
        css: {
            width: '100%',
            height: '100%',
            borderRadius: '20px'
        }
    }).appendTo('#medhelpDivContainer');
}