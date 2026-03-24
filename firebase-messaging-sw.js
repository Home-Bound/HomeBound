// Importamos los scripts de Firebase especiales para el fondo
importScripts('https://www.gstatic.com/firebasejs/9.6.10/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/9.6.10/firebase-messaging-compat.js');

// 🚨 REEMPLAZA ESTO CON LA CONFIGURACIÓN REAL DE TU PROYECTO 🚨
// (Es la misma que tienes en tu index.html)
const firebaseConfig = {
            apiKey: "AIzaSyC9Gf9YBWVYaNeCRCPt-2yfwQhJ40hHlo0",
            authDomain: "homebound-5ed20.firebaseapp.com",
            projectId: "homebound-5ed20",
            storageBucket: "homebound-5ed20.firebasestorage.app",
            messagingSenderId: "463623910254",
            appId: "1:463623910254:web:b05c03ee06eaa0b935477f",
};

// Inicializamos la app en el fondo
firebase.initializeApp(firebaseConfig);
const messaging = firebase.messaging();

// Esta función es la que hace sonar el celular cuando la app está cerrada
messaging.onBackgroundMessage(function(payload) {
    console.log("🔔 [Fondo] Notificación recibida:", payload);

    const notificationTitle = payload.notification.title;
    const notificationOptions = {
        body: payload.notification.body,
        icon: '/icon-192.png', // El icono de HomeBound
        badge: '/icon-512.png' // El icono chiquito de la barra superior
    };

    return self.registration.showNotification(notificationTitle, notificationOptions);
});