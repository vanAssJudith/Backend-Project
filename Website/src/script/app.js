(async function() {
    console.log('Pagina geladen');
    const data = await dataModule.doAPICall('https://localhost:44328/api/Quiz');
    console.log(data);

    const quizObs = [];

    let quizHTML = '';
    for (const quiz of quizObs){
        quizHTML += quiz.generateHTML();
    }
    document.querySelector('.js-holder').innerHTML = quizHTML;
})();