
var accountSid = 'ACc6463eba326ed17ee831fe4d4969084d'; // Your Account SID from www.twilio.com/console
var authToken = '46e74265079f6d297efff85a8459abde';   // Your Auth Token from www.twilio.com/console

var twilio = require('twilio');
var client = new twilio(accountSid, authToken);

client.messages.create({
    body: 'SafePassaport: Me encuentro bien, puedes llamar al centro de refugiados,gracias',
    to: '+529611165528',  // Text this number
    from: '+523341600912' // From a valid Twilio number
})
.then((message) => console.log(message.sid));
