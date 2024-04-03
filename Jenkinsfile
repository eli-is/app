pipeline {
    agent any
    
    stages {
        stage('Clean Workspace') {
    steps {
        // Clean workspace
        cleanWs()
    }
}
        stage('Checkout') {
            steps {
              // Download code from Git
        git branch: 'main', credentialsId: 'github', url: 'https://github.com/eli-is/app.git'
            }
        }
        
        stage('Build .NET Core Application & Build Docker Image') {
    steps {
        script {
            // Build Docker image
            docker.build("eli7890/app")
        }
    }
}
      stage('Push Docker Image to Docker Hub') {
    steps {
        script {
            // Push Docker image to Docker Hub
            docker.withRegistry('https://index.docker.io/v1/', 'docker-hub-credentials') {
                docker.image("eli7890/app").push()
            }
        }
    }
         }
      stage('Apply Kubernetes Configuration to Minikube') {
    steps {
        script {
            // Set the kubectl context to Minikube
            sh 'kubectl config use-context minikube'

            // Apply service
            sh 'kubectl apply -f service-app.yaml -n app'

            // Apply deployment
            sh 'kubectl apply -f deployment-app.yaml -n app'
        }
    }
}

}
}  
