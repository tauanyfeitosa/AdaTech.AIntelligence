document.addEventListener('DOMContentLoaded', function () {
    const requestButton = document.getElementById('requestButton');
    const rolesSelect = document.getElementById('roles');
    const form = document.getElementById('promotionForm');

    requestButton.addEventListener('click', function () {
        buscarCargos();
    });

    function buscarCargos() {
        fetch('https://localhost:7016/api/Promotion/roles-for-promotion')
            .then(response => response.json())
            .then(data => {
                popularCargos(data);
            })
            .catch(error => {
                console.error('Erro ao buscar cargos:', error);
            });
    }

    function popularCargos(resposta) {
        rolesSelect.innerHTML = '';
        const cargos = resposta.$values;

        if (Array.isArray(cargos)) {
            cargos.forEach(function (valorDoCargo) {
                let option = document.createElement('option');
                option.value = valorDoCargo;
                option.textContent = mapearValorParaNome(valorDoCargo);
                rolesSelect.appendChild(option);
            });
        } else {
            console.error('A resposta recebida não contém um array válido:', cargos);
        }
    }

    function mapearValorParaNome(valor) {
        const mapeamento = {
            1: 'Admin',
            2: 'Finance',
            3: 'Employee'
        };
        return mapeamento[valor] || 'Cargo desconhecido';
    }

    form.addEventListener('submit', function (e) {
        e.preventDefault();
        solicitarPromocao();
        modal.style.display = "none";
    });

    function solicitarPromocao() {
        const selectedRole = parseInt(rolesSelect.value);
        const url = new URL('https://localhost:7016/api/Promotion/ask-for-promotion');
        url.searchParams.append('role', selectedRole);

        fetch(url, { 
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Falha na solicitação: ' + response.statusText);
                }
                return response.json(); 
            })
            .then(data => {
                alert('Solicitação enviada');
            })
            .catch(error => {
                console.error('Erro ao solicitar promoção:', error);
                alert('Erro ao enviar solicitação: ' + error.message);
            });
    }
});
