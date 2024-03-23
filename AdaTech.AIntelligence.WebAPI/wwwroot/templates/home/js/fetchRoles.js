document.addEventListener('DOMContentLoaded', function () {
    const requestButton = document.getElementById('requestButton');
    const rolesSelect = document.getElementById('roles');

    requestButton.addEventListener('click', function () {
        buscarCargos();
    });

    function buscarCargos() {
        fetch('https://localhost:7016/api/Promotion/roles-for-promotion') 
            .then(response => response.json())
            .then(data => {
                console.log(data);
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

});
