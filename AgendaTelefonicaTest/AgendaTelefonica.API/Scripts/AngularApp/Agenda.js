var app = angular.module("Agenda", []);

app.controller("List", function ($scope, $http) {
    $scope.GetContactos = function () {
        $http({
            method: 'get',
            url: '/api/AgendaBack'
        }).then(function (response) {
            $scope.Contactos = response.data;
            console.log($scope.Contactos);
        }, function () {
                alert("Error");
        });
    }

    $scope.EliminarContacto = function (IdContacto) {
        var res = confirm("¿Seguro que desea eliminar este contacto?");
        if (res) {
            $http({
                method: 'delete',
                url: `/api/AgendaBack/${IdContacto}`
            }).then(function () {
                $scope.GetContactos();
            }, function () {
                    alert("Error");
            });
        }
    }
});

app.controller("Add", function ($scope, $http) {
    $scope.Nombre = '';
    $scope.PrimerApellido = '';
    $scope.SegundoApellido = '';
    $scope.Telefonos = [];

    $scope.GuardarContacto = function () {
        var contacto = {
            Nombre: $scope.Nombre,
            PrimerApellido: $scope.PrimerApellido,
            SegundoApellido: $scope.SegundoApellido,
            Telefonos: $scope.Telefonos
        }
        $http.post('/api/AgendaBack', contacto)
            .then(function (response) {
                console.log(response.data);
            }, function () {
                alert("Error");
            });
    }

    $scope.AgregarTelefono = function () {
        $scope.Telefonos.push({ InEdit: true, TipoTelefono: 1 });
        $scope.Telefonos = [...$scope.Telefonos];
    }

    $scope.EliminarTelefono = function (telefono) {
        var idx = $scope.Telefonos.findIndex(function (value) {
            return value.Numero === telefono.Numero;
        });
        if (idx > -1) {
            $scope.Telefonos.splice(idx, 1);
        }
        $scope.Telefonos = [...$scope.Telefonos];
    }
});

app.controller("Edit", function ($scope, $http) {
    $scope.IdContacto = $("#IdContacto").val();
    $scope.Nombre = '';
    $scope.PrimerApellido = '';
    $scope.SegundoApellido = '';
    $scope.Telefonos = [];

    $scope.CargarContacto = function () {
        $http.get(`/api/AgendaBack/${$scope.IdContacto}`)
            .then(function (response) {
                $scope.Nombre = response.data.Nombre;
                $scope.PrimerApellido = response.data.PrimerApellido;
                $scope.SegundoApellido = response.data.SegundoApellido;
                $scope.Telefonos = response.data.Telefonos;
            }, function () {
                alert("Error");
            });
    }

    $scope.GuardarContacto = function () {
        var contacto = {
            IdContacto: $scope.IdContacto,
            Nombre: $scope.Nombre,
            PrimerApellido: $scope.PrimerApellido,
            SegundoApellido: $scope.SegundoApellido,
            Telefonos: $scope.Telefonos
        }
        $http.put(`/api/AgendaBack/${$scope.IdContacto}`, contacto)
            .then(function (response) {
                console.log(response.data);
            }, function () {
                alert("Error");
            });
    }

    $scope.AgregarTelefono = function () {
        $scope.Telefonos.push({ InEdit: true, TipoTelefono: 1 });
        $scope.Telefonos = [...$scope.Telefonos];
    }

    $scope.EliminarTelefono = function (telefono) {
        var idx = $scope.Telefonos.findIndex(function (value) {
            return value.Numero === telefono.Numero;
        });
        if (idx > -1) {
            $scope.Telefonos.splice(idx, 1);
        }
        $scope.Telefonos = [...$scope.Telefonos];
    }
});
