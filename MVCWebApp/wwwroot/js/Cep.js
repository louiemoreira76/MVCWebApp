const cep = document.querySelector('#cep')
const msg = document.querySelector('#msg')
const endereco = document.querySelector('#endereco')

cep.addEventListener('focusout', async () => {
    try {
        console.log('focus out')
        const numOnly = /^[0-9]+$/
        const valid = /^[0-9]{8}$/

        if (!numOnly.test(cep.value) || !valid.test(cep.value)){ 
            throw { cep_error: 'Cep invalid' }
        }

        const viaCep = await fetch(`https://viacep.com.br/ws/${cep.value}/json/`)

        if (!viaCep.ok) {
            throw await viaCep.json()
        }

        const response = await viaCep.json()

        endereco.value = response.logradouro
    }
    catch (error) {
        if (error?.cep_error) {
            msg.textContent = error.cep_error

            setTimeout(() => {
                message.textContent = '';
            }, 5000)
        }
    }
})