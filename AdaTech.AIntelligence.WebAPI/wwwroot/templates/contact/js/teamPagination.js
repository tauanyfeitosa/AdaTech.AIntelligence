document.addEventListener('DOMContentLoaded', () => {
    const teamMembers = [
        { imgSrc: 'imgTeam/charles.jpeg', name: 'Charles Serafim', contact: 'https://www.linkedin.com/in/charles-serafim/' },
        { imgSrc: 'imgTeam/eduarda.jpeg', name: 'Maria Eduarda', contact: 'https://www.linkedin.com/in/maria-eduarda-sampaio-955087213/' },
        { imgSrc: 'imgTeam/tauany.jpg', name: 'Tauany Feitosa', contact: 'https://www.linkedin.com/in/tauanyfeitosa' },
        { imgSrc: 'imgTeam/miguel.jpeg', name: 'Miguel Pereira', contact: 'https://www.linkedin.com/in/miguelsousakoh/' },
        { imgSrc: 'imgTeam/yuri.jpeg', name: 'Yuri Cifuentes', contact: 'https://www.linkedin.com/in/yuri-cifuentes/' },
    ];

    let currentIndex = 0;

    function updateTeamMember(index) {
        const member = teamMembers[index];
        const memberDiv = document.getElementById('team-member');
        memberDiv.innerHTML = `
        <img src="${member.imgSrc}" alt="${member.name}" style="width:200px;height:200px;object-fit:cover;border-radius:50%;">
        <h2>${member.name}</h2>
        <p>Linkedin: <a href="${member.contact}" target="_blank">${member.name}</a></p>
    `;
    }


    document.getElementById('prev').addEventListener('click', () => {
        if (currentIndex > 0) {
            currentIndex--;
            updateTeamMember(currentIndex);
        }
    });

    document.getElementById('next').addEventListener('click', () => {
        if (currentIndex < teamMembers.length - 1) {
            currentIndex++;
            updateTeamMember(currentIndex);
        }
    });

    updateTeamMember(0);
});
