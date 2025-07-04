function printContent(contentId) {
    var w = window.open('', '', 'height=auto,width=auto');

    w.document.write('<html><head><style>#invoice-POS{box-shadow:0 0 1in -0.25in rgba(0,0,0,0.5);padding:2mm;margin:0 auto;width:74mm;background:#fff;}::selection{background:#f31544;color:#fff;}::-moz-selection{background:#f31544;color:#fff;}h1{font-size:1.5em;color:#222;}h2{font-size:0.9em;}h3{font-size:1.2em;font-weight:300;line-height:2em;}p{font-size:0.7em;color:#666;line-height:1.2em;}#top,#mid,#bot{border-bottom:1px solid #eee;}#top{min-height:100px;}#mid{min-height:80px;}#bot{min-height:50px;}#top .logo{height:60px;width:60px;background:url(http://michaeltruong.ca/images/logo1.png) no-repeat;background-size:60px 60px;}.clientlogo{float:left;height:60px;width:60px;background:url(http://michaeltruong.ca/images/client.jpg) no-repeat;background-size:60px 60px;border-radius:50px;}.info{display:block;margin-left:0;}.title{float:right;}.title p{text-align:right;}table{width:100%;border-collapse:collapse;}td{}.tabletitle{font-size:0.5em;background:#eee;}.service{border-bottom:1px solid #eee;font-size:30px !important}.item{width:24mm;}.itemtext{font-size:0.5em;}#legalcopy{margin-top:5mm;} img{height:100px!important;width:100px!important}</style><title></title>');
    w.document.write('<link rel="stylesheet" type="text/css" href="css/receipt-print1.css">');
    w.document.write('</head><body >');
    w.document.write(document.getElementById(contentId).innerHTML);
    w.document.write('<script type="text/javascript">addEventListener("load", () => { print(); close(); })</script></body></html>');

    w.document.close();
    w.focus();
}
