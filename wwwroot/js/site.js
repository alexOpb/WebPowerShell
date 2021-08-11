var result;
var position = 0

async function ProcessRequest() {
    let command = document.getElementById("InputCommand").value;

    let response = await fetch('Commands/Process', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(command)
    });

    let result = await response.json();
    document.getElementById("Result").value = result.value;
}

async function GetAll() {

    const response = await fetch('Commands/GetAllCommands', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        }
    });

    result = await response.json();
    position = result.length
}

const input = document.getElementById('InputCommand');
input.addEventListener('keydown', async function (event) {
    const key = event.key;
    switch (key) {
        case "ArrowDown":
            if (result == null || result.length === 0 || position == result.length)
                return

            if (position < result.length - 1)
                position++

            document.getElementById("InputCommand").value = result[position].commandText;
            break;
        case "ArrowUp":
            if (result == null || result.length === 0)
                return

            if (position > 0)
                position--

            document.getElementById("InputCommand").value = result[position].commandText;
            break;
        case "Enter":
            let command = document.getElementById("InputCommand");
            if (command.value == null || command.value === '')
                return

            await AddNewCommand(command.value);
            command.value = null;
            break;
        default:
            break;
    }
});


async function AddNewCommand(command) {
    result.push({ "id": null, "commandText": command });
    position = result.length
    await ProcessRequest();
}