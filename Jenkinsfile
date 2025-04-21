pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                echo 'Building the project...'
                bat 'dotnet build'
            }
        }
        stage('Test') {
            steps {
                echo 'Running unit tests...'
                bat 'dotnet test'
            }
        }
    }
}
